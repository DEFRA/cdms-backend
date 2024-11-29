

using Cdms.Backend.Data;
using Cdms.Backend.Data.InMemory;
using Cdms.Business.Services;
using Cdms.Model;
using Cdms.Model.Alvs;
using Cdms.Model.Ipaffs;
using FluentAssertions;
using Xunit;
using Document = Cdms.Model.Alvs.Document;
using Items = Cdms.Model.Alvs.Items;

namespace Cdms.Business.Tests.Services;

public class LinkingServiceTests
{
    private static readonly Random random = new ();
    private static readonly IMongoDbContext dbContext;
    private static string GenerateDocumentReference(int id) => $"GBCVD2024.{id}";
    private static string GenerateNotificationReference(int id) => $"CHEDP.GB.2024.{id}";
    
    static LinkingServiceTests()
    {
        dbContext = new MemoryMongoDbContext();
    }

    [Fact]
    public async Task Link_UnknownContextType_ShouldThrowException()
    {
        // Arrange
        var sut = new LinkingService(dbContext);
        var ctx = new BadContext();
        
        // Act
        var test = () => sut.Link(ctx);

        // Assert
        await test.Should().ThrowAsync<ArgumentException>();
    }
    
    [Fact]
    public async Task LinkMovement_ExistingRequest_IncludesFieldsOfInterest_MatchingCHEDs_AddsAllToLinkResult()
    {
        // Arrange
        var testData = await AddTestData();
        
        var sut = new LinkingService(dbContext);
        var movementCtx = CreateMovementContext(testData.Movements[0], [testData.Cheds[0], null], true, true);

        // Act
        var linkResult = await sut.Link(movementCtx);

        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.Linked);
        linkResult.Notifications.Count.Should().Be(1);
        linkResult.Movements.Count.Should().Be(1);
    }
    
    [Fact]
    public async Task LinkMovement_ExistingRequest_IncludesFieldsOfInterest_NoMatchingCHEDs_NoMatchingTriggered()
    {
        // Arrange
        var testData = await AddTestData();

        var sut = new LinkingService(dbContext);
        var movementCtx = CreateMovementContext(testData.Movements[0], [null], true, true);
        
        // Act
        var linkResult = await sut.Link(movementCtx);
        
        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.NotLinked);
        linkResult.Notifications.Count.Should().Be(0);
        linkResult.Movements.Count.Should().Be(1);
    }

    [Fact]
    public async Task LinkMovement_ExistingRequest_NoFieldsOfInterest_NoMatchingTriggered()
    {
        // Arrange
        var testData = await AddTestData();
        
        var sut = new LinkingService(dbContext);
        var movementCtx = CreateMovementContext(testData.Movements[0], [testData.Cheds[0]], true, false);
        
        // Act
        var linkResult = await sut.Link(movementCtx);
        
        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.NotLinked);
        linkResult.Notifications.Count.Should().Be(0);
        linkResult.Movements.Count.Should().Be(0);
    }

    [Fact]
    public async Task LinkMovement_NewRequest_MatchingCHED_AddsAllToLinkResult()
    {
        // Arrange
        var testData = await AddTestData();
        
        var sut = new LinkingService(dbContext);
        var movementCtx = CreateMovementContext(null, [testData.Cheds[0]], true, true);
        
        // Act
        var linkResult = await sut.Link(movementCtx);
        
        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.Linked);
        linkResult.Notifications.Count.Should().Be(1);
        linkResult.Movements.Count.Should().Be(1);
    }
    
    [Fact]
    public async Task LinkMovement_NewRequest_MultipleMatchingCHEDs_AddsAllToLinkResult()
    {
        // Arrange
        var testData = await AddTestData(2, 1, 2);

        var sut = new LinkingService(dbContext);
        var movementCtx = CreateMovementContext(null, [testData.Cheds[0], testData.Cheds[1]], false, false);
        
        // Act
        var linkResult = await sut.Link(movementCtx);
        
        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.Linked);
        linkResult.Notifications.Count.Should().Be(2);
        linkResult.Movements.Count.Should().Be(1);
    }
    
    [Fact]
    public async Task LinkMovement_NewRequest_NoMatchingCHEDs_NoMatchingTriggered()
    {
        // Arrange
        var sut = new LinkingService(dbContext);
        var movementCtx = CreateMovementContext(null, [null], true, true);
        
        // Act
        var linkResult = await sut.Link(movementCtx);
        
        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.NotLinked);
        linkResult.Notifications.Count.Should().Be(0);
        linkResult.Movements.Count.Should().Be(1);
    }
    
    [Fact]
    public async Task LinkNotification_ExistingNotification_IncludesFieldsOfInterest_MatchingMRN_AddsAllToLinkResult()
    {
        // Arrange
        var testData = await AddTestData();

        var sut = new LinkingService(dbContext);
        var notificationCtx = CreateNotificationContext(testData.Cheds[0], true, true);
        
        // Act
        var linkResult = await sut.Link(notificationCtx);
        
        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.Linked);
        linkResult.Notifications.Count.Should().Be(1);
        linkResult.Movements.Count.Should().Be(1);
    }

    [Fact]
    public async Task LinkNotification_ExistingNotification_IncludesFieldsOfInterest_MultipleMatchingMRNs_AddsAllToLinkResult()
    {
        // Arrange
        var testData = await AddTestData(2,2,2);
        
        var sut = new LinkingService(dbContext);
        var notificationCtx = CreateNotificationContext(testData.Cheds[0], true, true);
        
        // Act
        var linkResult = await sut.Link(notificationCtx);
        
        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.Linked);
        linkResult.Notifications.Count.Should().Be(1);
        linkResult.Movements.Count.Should().Be(2);
    }
    
    [Fact]
    public async Task LinkNotification_ExistingNotification_IncludesFieldsOfInterest_NoMatchingMRNs_NoMatchingTriggered()
    {
        // Arrange
        var testData = await AddTestData(1, 1, 0);
        
        var sut = new LinkingService(dbContext);
        var notificationCtx = CreateNotificationContext(testData.Cheds[0], true, true);
        
        // Act
        var linkResult = await sut.Link(notificationCtx);
        
        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.NotLinked);
        linkResult.Notifications.Count.Should().Be(1);
        linkResult.Movements.Count.Should().Be(0);
    }

    [Fact]
    public async Task LinkNotification_ExistingNotification_NoFieldsOfInterest_NoMatchingTriggered()
    {
        // Arrange
        var testData = await AddTestData(2, 2, 2);
        
        var sut = new LinkingService(dbContext);
        var notificationCtx = CreateNotificationContext(testData.Cheds[0], true, false);
        
        // Act
        var linkResult = await sut.Link(notificationCtx);
        
        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.NotLinked);
        linkResult.Notifications.Count.Should().Be(0);
        linkResult.Movements.Count.Should().Be(0);
    }

    [Fact]
    public async Task LinkNotification_NewNotification_MatchingMRN_AddsAllToLinkResult()
    {
        // Arrange
        var testData = await AddTestData(0,4,0, 1);
        
        var sut = new LinkingService(dbContext);
        var notificationCtx = CreateNotificationContext(testData.UnmatchedChedRefs[0], string.Empty, false, true);
        
        // Act
        var linkResult = await sut.Link(notificationCtx);
        
        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.Linked);
        linkResult.Notifications.Count.Should().Be(1);
        linkResult.Movements.Count.Should().Be(4);
    }

    [Fact]
    public async Task LinkNotification_NewNotification_MultipleMatchingMRNs_AddsAllToLinkResult()
    {
        // Arrange
        var testData = await AddTestData(0,1,0, 1);
        
        var sut = new LinkingService(dbContext);
        var notificationCtx = CreateNotificationContext(testData.UnmatchedChedRefs[0], string.Empty, false, true);
        
        // Act
        var linkResult = await sut.Link(notificationCtx);
        
        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.Linked);
        linkResult.Notifications.Count.Should().Be(1);
        linkResult.Movements.Count.Should().Be(1);
    }
    
    [Fact]
    public async Task LinkNotification_NewNotification_NoMatchingMRNs_NoMatchingTriggered()
    {
        // Arrange
        var sut = new LinkingService(dbContext);
        var notificationCtx = CreateNotificationContext(null, false, false);
        
        // Act
        var linkResult = await sut.Link(notificationCtx);
        
        // Assert
        linkResult.Should().NotBeNull();
        linkResult.State.Should().Be(LinkState.NotLinked);
        linkResult.Notifications.Count.Should().Be(1);
        linkResult.Movements.Count.Should().Be(0);
    }

    private MovementLinkContext CreateMovementContext(Movement? movement, List<ImportNotification?> cheds, bool createExistingMovement, bool fieldsOfInterest)
    {
        var entryReference = movement != null ? movement.EntryReference : $"TEST{GenerateRandomReference()}";
        var etag = movement != null ? movement._Etag : string.Empty;
        var chedReferences = cheds
            .Select(x => x != null ? x._MatchReference : $"{GenerateRandomReference()}")
            .Select(y => int.Parse(y)).ToList();
        
        var mov = new Movement()
        {
            Id = entryReference,
            EntryReference = entryReference,
            _Etag = etag,
            Items = chedReferences.Select(x => new Items()
            {
                Documents = [ new Document { DocumentReference = GenerateDocumentReference(x) } ]
            }).ToList()
        };

        var existingMovement = createExistingMovement ? 
            new Movement()
            {
                Id = entryReference,
                EntryReference = entryReference,
                Items = chedReferences.Select(x => new Items()
                {
                    Documents = fieldsOfInterest
                        ? []
                        : [ new Document { DocumentReference = GenerateDocumentReference(x) } ]
                }).ToList()
            } : null;
        
        var output = LinkContext.ForMovement(mov, existingMovement);
        
        return output;
    }

    private ImportNotificationLinkContext CreateNotificationContext(ImportNotification? ched,
        bool createExistingNotification, bool fieldsOfInterest)
    {
        int chedReference = ched != null ? int.Parse(ched._MatchReference) : GenerateRandomReference();
        var etag = ched != null ? ched._Etag : string.Empty;

        return CreateNotificationContext(chedReference, etag, createExistingNotification, fieldsOfInterest);
    }
    
    private ImportNotificationLinkContext CreateNotificationContext(int chedReference, string etag, bool createExistingNotification, bool fieldsOfInterest)
    {
        var notification = new ImportNotification()
        {
            Id = GenerateNotificationReference(chedReference),
            Updated = DateTime.UtcNow,
            _Etag = etag,
            Commodities =
                [ new CommodityComplement { CommodityId = "1234567", CommodityDescription = "Definitely real things" }]
        };

        var existingNotification = createExistingNotification
            ? new ImportNotification()
            {
                Id = GenerateNotificationReference(chedReference),
                Updated = DateTime.UtcNow,
                Commodities = fieldsOfInterest
                    ? [] : [ new CommodityComplement { CommodityId = "1234567", CommodityDescription = "Definitely real things" }]
            }
            : null;

        var output = LinkContext.ForImportNotification(notification, existingNotification);

        return output;
    }
    
    private async Task<(List<ImportNotification> Cheds, List<Movement> Movements, List<int> UnmatchedChedRefs)> AddTestData(int chedCount = 1, int movementCount = 1, int matchedChedsPerMovement = 1,  int unMatchedChedsPerMovement = 0)
    {
        matchedChedsPerMovement = int.Min(matchedChedsPerMovement, chedCount);
        var movements = new List<Movement>();
        var cheds = new List<ImportNotification>();
        
        var unmatchedChedRefs = Enumerable
            .Range(0, unMatchedChedsPerMovement)
            .Select(_ => GenerateRandomReference()).ToList();
        
        for (int i = 0; i < chedCount; i++)
        {
            var matchingRef = GenerateRandomReference();
            var ched = new ImportNotification()
            {
                Updated = DateTime.UtcNow.AddHours(-1),
                ReferenceNumber = GenerateNotificationReference(matchingRef),
                Commodities = []
            };
            
            cheds.Add(ched);
            
            await dbContext.Notifications.Insert(ched);
        }

        for (int i = 0; i < movementCount; i++)
        {
            var entryRef = $"TESTREF{GenerateRandomReference()}";
            var mov = new Movement()
            {
                Id = entryRef,
                EntryReference = entryRef,
                ClearanceRequests =
                [
                    new AlvsClearanceRequest
                    {
                        Header = new() { EntryReference = entryRef, EntryVersionNumber = 3, DeclarationType = "F" }
                    }
                ],
                Items = new ()
            };
            
            movements.Add(mov);
            
            for (int j = 0; j < matchedChedsPerMovement; j++)
            {
                var matchRef = cheds[j]._MatchReference;
                var refNo = int.Parse(matchRef);
                
                mov.Items.Add(
                    new Items()
                    {
                        Documents =
                        [
                            new Document() { DocumentReference = GenerateDocumentReference(refNo) }
                        ]
                    });
            }
            
            foreach (var refNo in unmatchedChedRefs)
            {
                mov.Items.Add(
                    new Items()
                    {
                        Documents =
                        [
                            new Document() { DocumentReference = GenerateDocumentReference(refNo) }
                        ]
                    });
            }

            await dbContext.Movements.Insert(mov);
        }
        
        return (cheds, movements, unmatchedChedRefs);
    }
    
    private static int GenerateRandomReference()
    {
        string intString = "1";
        
        for (int i = 0; i < 6; i++)
        {
            intString += random.Next(9).ToString();
        }
        
        return int.Parse(intString);
    }
}

public record BadContext : LinkContext;