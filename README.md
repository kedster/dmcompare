# DMSRuntimeComparer

A .NET Framework 4.8.2 WinForms application to compare two folder structures including file metadata and SQL database backups (`.bak` files). The app parses metadata, mounts SQL backups, runs queries to extract data, compares objects, and generates reports. It uses a clean architecture with separation of concerns, supporting modular comparison strategies.

---

## Table of Contents

- [Project Structure](#project-structure)
- [Models](#models)
- [Services](#services)
- [Comparison Strategies](#comparison-strategies)
- [Interfaces](#interfaces)
- [Helpers](#helpers)
- [User Interface](#user-interface)
- [Getting Started](#getting-started)
- [Future Work](#future-work)

---

## Project Structure

- **Models** — Core domain objects representing folders, files, metadata, SQL objects, and comparison results.
- **Services** — Business logic for folder traversal, metadata parsing, SQL mounting, querying, and report generation.
- **Comparison Strategies** — Implementations of various file/object comparison algorithms (checksum, timestamp, filename).
- **Interfaces** — Abstractions defining contracts for key components to enable modularity and easier testing.
- **Helpers** — Utility classes for file system operations and SQL connection management.
- **UI** — Windows Forms user interface with synchronized comparison views and controls for running, clearing, and exporting reports.

---

## Models

- `FolderNode.cs` — Represents folders and files recursively as a tree.
- `Metadata.cs` — Stores file metadata including size, modification date, checksum.
- `SqlObject.cs` — Holds SQL table or query result data.
- `ComparisonResult.cs` — Contains results of comparing two files or objects, including differences.

---

## Services

- `FolderComparer.cs` — Builds folder trees and compares files based on metadata.
- `MetadataParser.cs` — Calculates file checksums and populates metadata.
- `SqlMountService.cs` — Stub service to mount/restore `.bak` SQL backups.
- `SqlQueryService.cs` — Stub for running queries against mounted databases.
- `ReportGenerator.cs` — Generates CSV reports from comparison results.

---

## Comparison Strategies

- `IComparisonStrategy.cs` — Interface defining how two metadata objects should be compared.
- `FileNameComparisonStrategy.cs` — Compares based on file name equality.
- `ChecksumComparisonStrategy.cs` — Compares based on file content checksum.
- `TimestampComparisonStrategy.cs` — Compares modification timestamps with tolerance.

---

## Interfaces

Defines contracts for key services:

- `IComparisonStrategy` — Compare two metadata objects.
- `ISqlMount` — Mount/restore SQL backups.
- `IReportGenerator` — Export reports.
- `IMetadataParser` — Parse and extract metadata from files.

---

## Helpers

- `FileSystemHelper.cs` — Common filesystem utilities (recursive file listing, safe path operations).
- `SqlConnectionHelper.cs` — Wrapper for managing SQL Server connections.

---

## User Interface

- `Mainform.cs` / `Mainform.Designer.cs` — Main Windows Forms UI containing:
  - Folder selection inputs
  - Buttons: Run Comparison, Clear, Export Report
  - Progress bar
  - Two synchronized DataGridViews in a custom ComparisonView user control for side-by-side result comparison

---

## Getting Started

1. Build the solution in Visual Studio targeting .NET Framework 4.8.2.
2. Use the UI to select two folders containing files and `.bak` backups.
3. Run the comparison:
   - The app scans folders recursively.
   - Parses metadata, calculates checksums.
   - Mounts `.bak` files (stubbed).
   - Runs queries to extract SQL objects (stubbed).
   - Compares files and SQL objects using selected comparison strategies.
   - View comparison results side-by-side.
   - Export comparison reports as CSV.

---

## Future Work

- Implement real SQL backup mounting/restoring using SMO or sqlcmd.
- Execute real SQL queries and populate `SqlObject` data.
- Add composite comparison strategy for multi-level diff.
- Add async processing and progress reporting improvements.
- Enhance UI with dynamic tabs and filtering.
- Support additional report formats (Excel, PDF).