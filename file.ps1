# Base project path
$basePath = "DMSRuntimeComparer"
New-Item -ItemType Directory -Path $basePath -Force

# Folder map
$folders = @{
    "Models" = @(
        "FolderNode.cs", "Metadata.cs", "SqlObject.cs", "ComparisonResult.cs"
    )
    "Services" = @(
        "FolderComparer.cs", "MetadataParser.cs",
        "SqlMountService.cs", "SqlQueryService.cs", "ReportGenerator.cs"
    )
    "Strategies" = @(
        "FileNameComparisonStrategy.cs", "ChecksumComparisonStrategy.cs", "TimestampComparisonStrategy.cs"
    )
    "Interfaces" = @(
        "IComparisonStrategy.cs", "ISqlMount.cs", "IReportGenerator.cs", "IMetadataParser.cs"
    )
    "Helpers" = @(
        "FileSystemHelper.cs", "SqlConnectionHelper.cs"
    )
    "UI" = @(
        "MainForm.cs", "MainForm.Designer.cs", "ComparisonView.cs"
    )
}

# Create folders and files
foreach ($folder in $folders.Keys) {
    $folderPath = Join-Path $basePath $folder
    New-Item -ItemType Directory -Path $folderPath -Force

    foreach ($file in $folders[$folder]) {
        $filePath = Join-Path $folderPath $file
        New-Item -ItemType File -Path $filePath -Force | Out-Null
    }
}

# Root files (project + entry point)
$rootFiles = @(
    "DMSRuntimeComparer.csproj",
    "Program.cs",
    "README.md"
)

foreach ($file in $rootFiles) {
    $filePath = Join-Path $basePath $file
    New-Item -ItemType File -Path $filePath -Force | Out-Null
}

Write-Host "✅ Project structure created at: $basePath" -ForegroundColor Green
