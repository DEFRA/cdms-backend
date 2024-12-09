using CdmsBackend.Cli.Features.GenerateModels.GenerateIpaffsModel.Builders;
using CdmsBackend.Cli.Features.GenerateModels.GenerateIpaffsModel.Commands;
using CommandLine;
using MediatR;

namespace CdmsBackend.Cli.Features.GenerateModels.GenerateImportNotificationModel.Commands
{
    [Verb("generate-import-notification", isDefault: false, HelpText = "Generates Csharp Ipaffs classes from Json Schema.")]
    class GenerateImportNotificationCommand : IRequest
    {
        [Option('s', "schema", Required = true,
            HelpText = "The Json schema file, which to use to generate the csharp classes.")]
        public string SchemaFile { get; set; }

        // [Option('o', "sourceOutputPath", Required = true, HelpText = "The path to save the generated csharp classes.")]
        public string SourceOutputPath { get; set; } = "D:\\repos\\esynergy\\cdms-backend\\Cdms.Types.Ipaffs.V1\\";

        // [Option('i', "internalOutputPath", Required = true, HelpText = "The path to save the generated csharp classes.")]
        public string InteralOutputPath { get; set; } = "D:\\repos\\esynergy\\cdms-backend\\Cdms.Model\\Ipaffs\\";

        // [Option('i', "internalOutputPath", Required = true, HelpText = "The path to save the generated csharp classes.")]
        public string MappingOutputPath { get; set; } =
            "D:\\repos\\esynergy\\cdms-backend\\Cdms.Types.Ipaffs.Mapping.V1\\";

        public class Handler : AsyncRequestHandler<GenerateIpaffsModelCommand>
        {
            protected override async Task Handle(GenerateIpaffsModelCommand request,
                CancellationToken cancellationToken)
            {
                var builder =
                    new ImportNotificationDescriptorBuilder(new List<ISchemaVisitor>() { new DescriptorBuilderSchemaVisitor() });

                var model = builder.Build(await File.ReadAllTextAsync(request.SchemaFile, cancellationToken));

                await CSharpFileBuilder.Build(model, request.SourceOutputPath, request.InteralOutputPath,
                    request.MappingOutputPath);
            }
        }
    }
}