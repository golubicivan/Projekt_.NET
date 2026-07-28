using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class IdentityVerificationRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IdentityRequired",
                table: "Venues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IdentityVerified",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 596,
                columns: new[] { "SeatCount", "TableNumber", "VenueId" },
                values: new object[] { 8, 1, 101 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 597,
                columns: new[] { "SeatCount", "TableNumber", "VenueId" },
                values: new object[] { 8, 2, 101 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 598,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 3, 101, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 599,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 4, 101, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 600,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 5, 101, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 601,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 6, 101, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 602,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 7, 101, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 603,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 8, 101, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 604,
                columns: new[] { "SeatCount", "TableNumber", "VenueId" },
                values: new object[] { 6, 9, 101 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 605,
                columns: new[] { "SeatCount", "TableNumber", "VenueId" },
                values: new object[] { 6, 10, 101 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 606,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 11, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 607,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 12, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 608,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 13, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 609,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 14, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 610,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 15, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 611,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 16, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 612,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 17, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 613,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 18, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 614,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 19, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 615,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 20, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 616,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 21, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 617,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 22, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 618,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 23, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 619,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 24, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 620,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 25, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 621,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 26, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 622,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 27, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 623,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 28, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 624,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 29, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 625,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 30, 101, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 626,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 1, 102 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 627,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 2, 102 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 628,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 3, 102 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 629,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 4, 102 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 630,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 5, 102 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 631,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 6, 102 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 632,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 7, 102 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 633,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 8, 102 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 634,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 9, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 635,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 10, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 636,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 11, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 637,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 12, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 638,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 13, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 639,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 14, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 640,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 15, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 641,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 16, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 642,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 17, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 643,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 6, 18, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 644,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 19, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 645,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 20, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 646,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 21, 102, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 647,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 22, 102, 0 });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "PosX", "PosY", "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[,]
                {
                    { 648, null, null, 4, 23, 102, 0 },
                    { 649, null, null, 4, 24, 102, 0 },
                    { 650, null, null, 4, 25, 102, 0 },
                    { 651, null, null, 4, 26, 102, 0 },
                    { 652, null, null, 4, 27, 102, 0 },
                    { 653, null, null, 4, 28, 102, 0 },
                    { 654, null, null, 4, 29, 102, 0 },
                    { 655, null, null, 4, 30, 102, 0 },
                    { 656, null, null, 8, 1, 103, 1 },
                    { 657, null, null, 8, 2, 103, 1 },
                    { 658, null, null, 8, 3, 103, 1 },
                    { 659, null, null, 8, 4, 103, 1 },
                    { 660, null, null, 8, 5, 103, 1 },
                    { 661, null, null, 8, 6, 103, 1 },
                    { 662, null, null, 8, 7, 103, 1 },
                    { 663, null, null, 8, 8, 103, 1 },
                    { 664, null, null, 6, 9, 103, 0 },
                    { 665, null, null, 6, 10, 103, 0 },
                    { 666, null, null, 6, 11, 103, 0 },
                    { 667, null, null, 6, 12, 103, 0 },
                    { 668, null, null, 6, 13, 103, 0 },
                    { 669, null, null, 6, 14, 103, 0 },
                    { 670, null, null, 6, 15, 103, 0 },
                    { 671, null, null, 6, 16, 103, 0 },
                    { 672, null, null, 6, 17, 103, 0 },
                    { 673, null, null, 6, 18, 103, 0 },
                    { 674, null, null, 4, 19, 103, 0 },
                    { 675, null, null, 4, 20, 103, 0 },
                    { 676, null, null, 4, 21, 103, 0 },
                    { 677, null, null, 4, 22, 103, 0 },
                    { 678, null, null, 4, 23, 103, 0 },
                    { 679, null, null, 4, 24, 103, 0 },
                    { 680, null, null, 4, 25, 103, 0 },
                    { 681, null, null, 4, 26, 103, 0 },
                    { 682, null, null, 4, 27, 103, 0 },
                    { 683, null, null, 4, 28, 103, 0 },
                    { 684, null, null, 4, 29, 103, 0 },
                    { 685, null, null, 4, 30, 103, 0 },
                    { 686, null, null, 8, 1, 104, 1 },
                    { 687, null, null, 8, 2, 104, 1 },
                    { 688, null, null, 8, 3, 104, 1 },
                    { 689, null, null, 8, 4, 104, 1 },
                    { 690, null, null, 8, 5, 104, 1 },
                    { 691, null, null, 8, 6, 104, 1 },
                    { 692, null, null, 8, 7, 104, 1 },
                    { 693, null, null, 8, 8, 104, 1 },
                    { 694, null, null, 6, 9, 104, 0 },
                    { 695, null, null, 6, 10, 104, 0 },
                    { 696, null, null, 6, 11, 104, 0 },
                    { 697, null, null, 6, 12, 104, 0 },
                    { 698, null, null, 6, 13, 104, 0 },
                    { 699, null, null, 6, 14, 104, 0 },
                    { 700, null, null, 6, 15, 104, 0 },
                    { 701, null, null, 6, 16, 104, 0 },
                    { 702, null, null, 6, 17, 104, 0 },
                    { 703, null, null, 6, 18, 104, 0 },
                    { 704, null, null, 4, 19, 104, 0 },
                    { 705, null, null, 4, 20, 104, 0 },
                    { 706, null, null, 4, 21, 104, 0 },
                    { 707, null, null, 4, 22, 104, 0 },
                    { 708, null, null, 4, 23, 104, 0 },
                    { 709, null, null, 4, 24, 104, 0 },
                    { 710, null, null, 4, 25, 104, 0 },
                    { 711, null, null, 4, 26, 104, 0 },
                    { 712, null, null, 4, 27, 104, 0 },
                    { 713, null, null, 4, 28, 104, 0 },
                    { 714, null, null, 4, 29, 104, 0 },
                    { 715, null, null, 4, 30, 104, 0 },
                    { 716, null, null, 8, 1, 105, 1 },
                    { 717, null, null, 8, 2, 105, 1 },
                    { 718, null, null, 8, 3, 105, 1 },
                    { 719, null, null, 8, 4, 105, 1 },
                    { 720, null, null, 8, 5, 105, 1 },
                    { 721, null, null, 8, 6, 105, 1 },
                    { 722, null, null, 8, 7, 105, 1 },
                    { 723, null, null, 8, 8, 105, 1 },
                    { 724, null, null, 6, 9, 105, 0 },
                    { 725, null, null, 6, 10, 105, 0 },
                    { 726, null, null, 6, 11, 105, 0 },
                    { 727, null, null, 6, 12, 105, 0 },
                    { 728, null, null, 6, 13, 105, 0 },
                    { 729, null, null, 6, 14, 105, 0 },
                    { 730, null, null, 6, 15, 105, 0 },
                    { 731, null, null, 6, 16, 105, 0 },
                    { 732, null, null, 6, 17, 105, 0 },
                    { 733, null, null, 6, 18, 105, 0 },
                    { 734, null, null, 4, 19, 105, 0 },
                    { 735, null, null, 4, 20, 105, 0 },
                    { 736, null, null, 4, 21, 105, 0 },
                    { 737, null, null, 4, 22, 105, 0 },
                    { 738, null, null, 4, 23, 105, 0 },
                    { 739, null, null, 4, 24, 105, 0 },
                    { 740, null, null, 4, 25, 105, 0 },
                    { 741, null, null, 4, 26, 105, 0 },
                    { 742, null, null, 4, 27, 105, 0 },
                    { 743, null, null, 4, 28, 105, 0 },
                    { 744, null, null, 4, 29, 105, 0 },
                    { 745, null, null, 4, 30, 105, 0 },
                    { 746, null, null, 8, 1, 106, 1 },
                    { 747, null, null, 8, 2, 106, 1 },
                    { 748, null, null, 8, 3, 106, 1 },
                    { 749, null, null, 8, 4, 106, 1 },
                    { 750, null, null, 8, 5, 106, 1 },
                    { 751, null, null, 8, 6, 106, 1 },
                    { 752, null, null, 8, 7, 106, 1 },
                    { 753, null, null, 8, 8, 106, 1 },
                    { 754, null, null, 6, 9, 106, 0 },
                    { 755, null, null, 6, 10, 106, 0 },
                    { 756, null, null, 6, 11, 106, 0 },
                    { 757, null, null, 6, 12, 106, 0 },
                    { 758, null, null, 6, 13, 106, 0 },
                    { 759, null, null, 6, 14, 106, 0 },
                    { 760, null, null, 6, 15, 106, 0 },
                    { 761, null, null, 6, 16, 106, 0 },
                    { 762, null, null, 6, 17, 106, 0 },
                    { 763, null, null, 6, 18, 106, 0 },
                    { 764, null, null, 4, 19, 106, 0 },
                    { 765, null, null, 4, 20, 106, 0 },
                    { 766, null, null, 4, 21, 106, 0 },
                    { 767, null, null, 4, 22, 106, 0 },
                    { 768, null, null, 4, 23, 106, 0 },
                    { 769, null, null, 4, 24, 106, 0 },
                    { 770, null, null, 4, 25, 106, 0 },
                    { 771, null, null, 4, 26, 106, 0 },
                    { 772, null, null, 4, 27, 106, 0 },
                    { 773, null, null, 4, 28, 106, 0 },
                    { 774, null, null, 4, 29, 106, 0 },
                    { 775, null, null, 4, 30, 106, 0 },
                    { 776, null, null, 8, 1, 107, 1 },
                    { 777, null, null, 8, 2, 107, 1 },
                    { 778, null, null, 8, 3, 107, 1 },
                    { 779, null, null, 8, 4, 107, 1 },
                    { 780, null, null, 8, 5, 107, 1 },
                    { 781, null, null, 8, 6, 107, 1 },
                    { 782, null, null, 8, 7, 107, 1 },
                    { 783, null, null, 8, 8, 107, 1 },
                    { 784, null, null, 6, 9, 107, 0 },
                    { 785, null, null, 6, 10, 107, 0 },
                    { 786, null, null, 6, 11, 107, 0 },
                    { 787, null, null, 6, 12, 107, 0 },
                    { 788, null, null, 6, 13, 107, 0 },
                    { 789, null, null, 6, 14, 107, 0 },
                    { 790, null, null, 6, 15, 107, 0 },
                    { 791, null, null, 6, 16, 107, 0 },
                    { 792, null, null, 6, 17, 107, 0 },
                    { 793, null, null, 6, 18, 107, 0 },
                    { 794, null, null, 4, 19, 107, 0 },
                    { 795, null, null, 4, 20, 107, 0 },
                    { 796, null, null, 4, 21, 107, 0 },
                    { 797, null, null, 4, 22, 107, 0 },
                    { 798, null, null, 4, 23, 107, 0 },
                    { 799, null, null, 4, 24, 107, 0 },
                    { 800, null, null, 4, 25, 107, 0 },
                    { 801, null, null, 4, 26, 107, 0 },
                    { 802, null, null, 4, 27, 107, 0 },
                    { 803, null, null, 4, 28, 107, 0 },
                    { 804, null, null, 4, 29, 107, 0 },
                    { 805, null, null, 4, 30, 107, 0 },
                    { 806, null, null, 8, 1, 108, 1 },
                    { 807, null, null, 8, 2, 108, 1 },
                    { 808, null, null, 8, 3, 108, 1 },
                    { 809, null, null, 8, 4, 108, 1 },
                    { 810, null, null, 8, 5, 108, 1 },
                    { 811, null, null, 8, 6, 108, 1 },
                    { 812, null, null, 8, 7, 108, 1 },
                    { 813, null, null, 8, 8, 108, 1 },
                    { 814, null, null, 6, 9, 108, 0 },
                    { 815, null, null, 6, 10, 108, 0 },
                    { 816, null, null, 6, 11, 108, 0 },
                    { 817, null, null, 6, 12, 108, 0 },
                    { 818, null, null, 6, 13, 108, 0 },
                    { 819, null, null, 6, 14, 108, 0 },
                    { 820, null, null, 6, 15, 108, 0 },
                    { 821, null, null, 6, 16, 108, 0 },
                    { 822, null, null, 6, 17, 108, 0 },
                    { 823, null, null, 6, 18, 108, 0 },
                    { 824, null, null, 4, 19, 108, 0 },
                    { 825, null, null, 4, 20, 108, 0 },
                    { 826, null, null, 4, 21, 108, 0 },
                    { 827, null, null, 4, 22, 108, 0 },
                    { 828, null, null, 4, 23, 108, 0 },
                    { 829, null, null, 4, 24, 108, 0 },
                    { 830, null, null, 4, 25, 108, 0 },
                    { 831, null, null, 4, 26, 108, 0 },
                    { 832, null, null, 4, 27, 108, 0 },
                    { 833, null, null, 4, 28, 108, 0 },
                    { 834, null, null, 4, 29, 108, 0 },
                    { 835, null, null, 4, 30, 108, 0 },
                    { 836, null, null, 4, 3, 8, 1 },
                    { 837, null, null, 4, 4, 8, 1 },
                    { 838, null, null, 4, 5, 8, 0 },
                    { 839, null, null, 4, 6, 8, 0 },
                    { 840, null, null, 4, 7, 8, 0 },
                    { 841, null, null, 4, 8, 8, 0 },
                    { 842, null, null, 4, 9, 8, 0 },
                    { 843, null, null, 4, 10, 8, 0 },
                    { 844, null, null, 4, 11, 8, 0 },
                    { 845, null, null, 4, 12, 8, 0 },
                    { 846, null, null, 8, 5, 3, 1 },
                    { 847, null, null, 8, 6, 3, 1 },
                    { 848, null, null, 8, 7, 3, 1 },
                    { 849, null, null, 8, 8, 3, 1 },
                    { 850, null, null, 8, 9, 3, 1 },
                    { 851, null, null, 8, 10, 3, 1 },
                    { 852, null, null, 8, 3, 20, 1 },
                    { 853, null, null, 8, 4, 20, 1 },
                    { 854, null, null, 8, 5, 20, 1 },
                    { 855, null, null, 8, 6, 20, 1 },
                    { 856, null, null, 8, 7, 20, 1 },
                    { 857, null, null, 8, 8, 20, 1 },
                    { 858, null, null, 8, 9, 20, 1 },
                    { 859, null, null, 8, 10, 20, 1 },
                    { 860, null, null, 8, 1, 21, 1 },
                    { 861, null, null, 8, 2, 21, 1 },
                    { 862, null, null, 8, 3, 21, 1 },
                    { 863, null, null, 8, 4, 21, 1 },
                    { 864, null, null, 8, 5, 21, 1 },
                    { 865, null, null, 8, 6, 21, 1 },
                    { 866, null, null, 8, 7, 21, 1 },
                    { 867, null, null, 8, 8, 21, 1 },
                    { 868, null, null, 8, 9, 21, 1 },
                    { 869, null, null, 8, 10, 21, 1 },
                    { 870, null, null, 8, 3, 22, 1 },
                    { 871, null, null, 8, 4, 22, 1 },
                    { 872, null, null, 8, 5, 22, 1 },
                    { 873, null, null, 8, 6, 22, 1 },
                    { 874, null, null, 8, 7, 22, 1 },
                    { 875, null, null, 8, 8, 22, 1 },
                    { 876, null, null, 8, 9, 22, 1 },
                    { 877, null, null, 8, 10, 22, 1 },
                    { 878, null, null, 8, 1, 23, 1 },
                    { 879, null, null, 8, 2, 23, 1 },
                    { 880, null, null, 8, 3, 23, 1 },
                    { 881, null, null, 8, 4, 23, 1 },
                    { 882, null, null, 8, 5, 23, 1 },
                    { 883, null, null, 8, 6, 23, 1 },
                    { 884, null, null, 8, 7, 23, 1 },
                    { 885, null, null, 8, 8, 23, 1 },
                    { 886, null, null, 8, 9, 23, 1 },
                    { 887, null, null, 8, 10, 23, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 1,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 2,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 3,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 4,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 5,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 6,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 7,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 8,
                column: "IdentityRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 9,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 10,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 11,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 12,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 13,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 14,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 15,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 16,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 17,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 18,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 19,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 20,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 21,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 22,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 23,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 24,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 25,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 26,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 27,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 28,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 29,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 30,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 31,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 101,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 102,
                column: "IdentityRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 103,
                column: "IdentityRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 104,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 105,
                column: "IdentityRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 106,
                column: "IdentityRequired",
                value: true);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 107,
                column: "IdentityRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 108,
                column: "IdentityRequired",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 648);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 649);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 650);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 651);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 652);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 653);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 654);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 655);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 656);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 657);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 658);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 659);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 660);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 661);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 662);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 663);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 664);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 665);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 666);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 667);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 668);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 669);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 670);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 671);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 672);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 673);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 674);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 675);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 676);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 677);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 678);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 679);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 680);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 681);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 682);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 683);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 684);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 685);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 686);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 687);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 688);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 689);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 690);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 691);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 692);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 693);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 694);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 695);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 696);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 697);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 698);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 699);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 700);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 701);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 702);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 703);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 704);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 705);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 706);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 707);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 708);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 709);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 710);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 711);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 712);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 713);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 714);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 715);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 716);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 717);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 718);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 719);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 720);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 721);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 722);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 723);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 724);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 725);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 726);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 727);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 728);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 729);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 730);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 731);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 732);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 733);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 734);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 735);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 736);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 737);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 738);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 739);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 740);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 741);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 742);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 743);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 744);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 745);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 746);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 747);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 748);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 749);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 750);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 751);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 752);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 753);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 754);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 755);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 756);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 757);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 758);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 759);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 760);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 761);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 762);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 763);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 764);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 765);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 766);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 767);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 768);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 769);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 770);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 771);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 772);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 773);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 774);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 775);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 776);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 777);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 778);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 779);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 780);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 781);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 782);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 783);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 784);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 785);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 786);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 787);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 788);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 789);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 790);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 791);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 792);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 793);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 794);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 795);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 796);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 797);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 798);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 799);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 800);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 801);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 802);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 803);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 804);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 805);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 806);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 807);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 808);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 809);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 810);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 811);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 812);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 813);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 814);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 815);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 816);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 817);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 818);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 819);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 820);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 821);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 822);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 823);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 824);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 825);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 826);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 827);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 828);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 829);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 830);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 831);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 832);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 833);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 834);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 835);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 836);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 837);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 838);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 839);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 840);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 841);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 842);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 843);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 844);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 845);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 846);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 847);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 848);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 849);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 850);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 851);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 852);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 853);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 854);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 855);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 856);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 857);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 858);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 859);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 860);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 861);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 862);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 863);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 864);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 865);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 866);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 867);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 868);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 869);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 870);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 871);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 872);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 873);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 874);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 875);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 876);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 877);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 878);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 879);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 880);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 881);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 882);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 883);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 884);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 885);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 886);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 887);

            migrationBuilder.DropColumn(
                name: "IdentityRequired",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "IdentityVerified",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 596,
                columns: new[] { "SeatCount", "TableNumber", "VenueId" },
                values: new object[] { 4, 3, 8 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 597,
                columns: new[] { "SeatCount", "TableNumber", "VenueId" },
                values: new object[] { 4, 4, 8 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 598,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 5, 8, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 599,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 6, 8, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 600,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 7, 8, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 601,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 8, 8, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 602,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 9, 8, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 603,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 4, 10, 8, 0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 604,
                columns: new[] { "SeatCount", "TableNumber", "VenueId" },
                values: new object[] { 4, 11, 8 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 605,
                columns: new[] { "SeatCount", "TableNumber", "VenueId" },
                values: new object[] { 4, 12, 8 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 606,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 5, 3, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 607,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 6, 3, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 608,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 7, 3, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 609,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 8, 3, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 610,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 9, 3, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 611,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 10, 3, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 612,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 3, 20, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 613,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 4, 20, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 614,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 5, 20, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 615,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 6, 20, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 616,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 7, 20, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 617,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 8, 20, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 618,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 9, 20, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 619,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 10, 20, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 620,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 1, 21, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 621,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 2, 21, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 622,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 3, 21, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 623,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 4, 21, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 624,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 5, 21, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 625,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 6, 21, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 626,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 7, 21 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 627,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 8, 21 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 628,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 9, 21 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 629,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 10, 21 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 630,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 3, 22 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 631,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 4, 22 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 632,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 5, 22 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 633,
                columns: new[] { "TableNumber", "VenueId" },
                values: new object[] { 6, 22 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 634,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 7, 22, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 635,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 8, 22, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 636,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 9, 22, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 637,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 10, 22, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 638,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 1, 23, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 639,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 2, 23, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 640,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 3, 23, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 641,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 4, 23, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 642,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 5, 23, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 643,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 6, 23, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 644,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 7, 23, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 645,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 8, 23, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 646,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 9, 23, 1 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 647,
                columns: new[] { "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[] { 8, 10, 23, 1 });
        }
    }
}
