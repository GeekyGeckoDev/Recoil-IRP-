using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Monotring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Playbooks_PlaybookId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "SourceRequstLogId",
                table: "Alerts");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlaybookId",
                table: "Incidents",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "EvidenceCollectionId",
                table: "Evidence",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RegisteredAt",
                table: "Alerts",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "EventProcessingRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecurityEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    ProcessedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventProcessingRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvidenceCollection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvidenceCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvidenceCollection_Incidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitoredAssets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoredAssets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AgentVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OperatingSystem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegisteredAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastHeartbeat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgentRegistrations_MonitoredAssets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "MonitoredAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollectorConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectorType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectorConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectorConfigurations_AgentRegistrations_AgentId",
                        column: x => x.AgentId,
                        principalTable: "AgentRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecurityEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalEventId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OccurredAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReceivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityEvents_AgentRegistrations_AgentId",
                        column: x => x.AgentId,
                        principalTable: "AgentRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityEvents_MonitoredAssets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "MonitoredAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evidence_EvidenceCollectionId",
                table: "Evidence",
                column: "EvidenceCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentRegistrations_AssetId",
                table: "AgentRegistrations",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentRegistrations_AssetId_IsActive",
                table: "AgentRegistrations",
                columns: new[] { "AssetId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_CollectorConfigurations_AgentId_CollectorType",
                table: "CollectorConfigurations",
                columns: new[] { "AgentId", "CollectorType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvidenceCollection_IncidentId",
                table: "EvidenceCollection",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoredAssets_Name",
                table: "MonitoredAssets",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityEvents_AgentId_ExternalEventId",
                table: "SecurityEvents",
                columns: new[] { "AgentId", "ExternalEventId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityEvents_AssetId_OccurredAt",
                table: "SecurityEvents",
                columns: new[] { "AssetId", "OccurredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_SecurityEvents_OccurredAt",
                table: "SecurityEvents",
                column: "OccurredAt");

            migrationBuilder.AddForeignKey(
                name: "FK_Evidence_EvidenceCollection_EvidenceCollectionId",
                table: "Evidence",
                column: "EvidenceCollectionId",
                principalTable: "EvidenceCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Playbooks_PlaybookId",
                table: "Incidents",
                column: "PlaybookId",
                principalTable: "Playbooks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evidence_EvidenceCollection_EvidenceCollectionId",
                table: "Evidence");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Playbooks_PlaybookId",
                table: "Incidents");

            migrationBuilder.DropTable(
                name: "CollectorConfigurations");

            migrationBuilder.DropTable(
                name: "EventProcessingRecords");

            migrationBuilder.DropTable(
                name: "EvidenceCollection");

            migrationBuilder.DropTable(
                name: "SecurityEvents");

            migrationBuilder.DropTable(
                name: "AgentRegistrations");

            migrationBuilder.DropTable(
                name: "MonitoredAssets");

            migrationBuilder.DropIndex(
                name: "IX_Evidence_EvidenceCollectionId",
                table: "Evidence");

            migrationBuilder.DropColumn(
                name: "EvidenceCollectionId",
                table: "Evidence");

            migrationBuilder.DropColumn(
                name: "RegisteredAt",
                table: "Alerts");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlaybookId",
                table: "Incidents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceRequstLogId",
                table: "Alerts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Playbooks_PlaybookId",
                table: "Incidents",
                column: "PlaybookId",
                principalTable: "Playbooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
