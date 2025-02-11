USE [master]
GO
/****** Object:  Database [NoiThat]    Script Date: 13/12/2019 5:35:11 PM ******/
CREATE DATABASE [NoiThat]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NoiThat', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\DATA\NoiThat.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NoiThat_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\DATA\NoiThat_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NoiThat].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NoiThat] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NoiThat] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NoiThat] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NoiThat] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NoiThat] SET ARITHABORT OFF 
GO
ALTER DATABASE [NoiThat] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NoiThat] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NoiThat] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NoiThat] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NoiThat] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NoiThat] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NoiThat] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NoiThat] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NoiThat] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NoiThat] SET  ENABLE_BROKER 
GO
ALTER DATABASE [NoiThat] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NoiThat] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NoiThat] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NoiThat] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NoiThat] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NoiThat] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NoiThat] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NoiThat] SET RECOVERY FULL 
GO
ALTER DATABASE [NoiThat] SET  MULTI_USER 
GO
ALTER DATABASE [NoiThat] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NoiThat] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NoiThat] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NoiThat] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'NoiThat', N'ON'
GO
USE [NoiThat]
GO
/****** Object:  Table [dbo].[ChungLoai]    Script Date: 13/12/2019 5:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChungLoai](
	[ChungLoaiID] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NULL,
	[BiDanh] [varchar](50) NULL,
	[Hinh] [varchar](10) NULL,
 CONSTRAINT [PK_ChungLoai] PRIMARY KEY CLUSTERED 
(
	[ChungLoaiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 13/12/2019 5:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HoaDon](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NgayDatHang] [datetime] NOT NULL CONSTRAINT [DF_hoadon_ngaydathang]  DEFAULT (getdate()),
	[HoTenKhach] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](150) NULL,
	[DienThoai] [varchar](30) NULL,
	[Email] [varchar](50) NULL,
	[TongTien] [int] NOT NULL CONSTRAINT [DF_HoaDon_TongTien]  DEFAULT ((0)),
 CONSTRAINT [PK_HoaDon_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HoaDonChiTiet]    Script Date: 13/12/2019 5:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonChiTiet](
	[HoaDonID] [int] NOT NULL,
	[SanPhamID] [int] NOT NULL,
	[SoLuong] [int] NOT NULL CONSTRAINT [DF_HoaDonChiTiet_SoLuong]  DEFAULT ((0)),
	[DonGia] [int] NOT NULL CONSTRAINT [DF_HoaDonChiTiet_DonGia]  DEFAULT ((0)),
	[ThanhTien] [int] NOT NULL CONSTRAINT [DF_HoaDonChiTiet_ThanhTien]  DEFAULT ((0)),
 CONSTRAINT [PK_HoaDonChiTiet] PRIMARY KEY CLUSTERED 
(
	[HoaDonID] ASC,
	[SanPhamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Loai]    Script Date: 13/12/2019 5:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Loai](
	[LoaiID] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NULL,
	[ChungLoaiID] [int] NULL,
	[BiDanh] [varchar](50) NULL,
	[PriceFrom] [numeric](18, 0) NULL,
	[Hinh] [varchar](50) NULL,
 CONSTRAINT [PK_Loai] PRIMARY KEY CLUSTERED 
(
	[LoaiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 13/12/2019 5:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[NhaCungCapID] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NULL,
	[GioiThieu] [nvarchar](max) NULL,
 CONSTRAINT [PK_NhaCungCap] PRIMARY KEY CLUSTERED 
(
	[NhaCungCapID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 13/12/2019 5:35:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SanPham](
	[SanPhamID] [int] IDENTITY(1,1) NOT NULL,
	[MaSo] [nvarchar](30) NULL,
	[Ten] [nvarchar](100) NULL,
	[MoTa] [nvarchar](max) NULL,
	[GiaBan] [int] NOT NULL CONSTRAINT [DF_SanPham_GiaBan]  DEFAULT ((0)),
	[NhaCungCapID] [int] NULL,
	[XuatXu] [nvarchar](50) NULL,
	[LoaiID] [int] NULL,
	[ChatLieu] [nvarchar](50) NULL,
	[MauSac] [nvarchar](50) NULL,
	[Hinh] [nvarchar](10) NULL,
	[HetHang] [bit] NOT NULL CONSTRAINT [DF_SanPham_HetHang]  DEFAULT ((0)),
	[BiDanh] [varchar](50) NULL,
	[star] [int] NULL,
	[ListHinh] [nvarchar](max) NULL,
	[GiamGia] [int] NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[SanPhamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[ChungLoai] ON 

INSERT [dbo].[ChungLoai] ([ChungLoaiID], [Ten], [BiDanh], [Hinh]) VALUES (1, N'Nội thất văn phòng', N'noi-that-van-phong', NULL)
INSERT [dbo].[ChungLoai] ([ChungLoaiID], [Ten], [BiDanh], [Hinh]) VALUES (2, N'Nội thất phòng khách', N'noi-that-phong-khach', NULL)
INSERT [dbo].[ChungLoai] ([ChungLoaiID], [Ten], [BiDanh], [Hinh]) VALUES (3, N'Nội thất phòng ăn', N'noi-that-phong-an', NULL)
INSERT [dbo].[ChungLoai] ([ChungLoaiID], [Ten], [BiDanh], [Hinh]) VALUES (4, N'Nội thất phòng tắm', N'noi-that-phong-tam', NULL)
INSERT [dbo].[ChungLoai] ([ChungLoaiID], [Ten], [BiDanh], [Hinh]) VALUES (5, N'Đồ trang trí', N'do-trang-tri', NULL)
INSERT [dbo].[ChungLoai] ([ChungLoaiID], [Ten], [BiDanh], [Hinh]) VALUES (6, N'Bếp và phụ kiện', N'bep-va-phu-kien', NULL)
INSERT [dbo].[ChungLoai] ([ChungLoaiID], [Ten], [BiDanh], [Hinh]) VALUES (7, N'Sân vườn', N'san-vuon', NULL)
INSERT [dbo].[ChungLoai] ([ChungLoaiID], [Ten], [BiDanh], [Hinh]) VALUES (8, N'Sân thượng', N'san-thuong', NULL)
SET IDENTITY_INSERT [dbo].[ChungLoai] OFF
SET IDENTITY_INSERT [dbo].[HoaDon] ON 

INSERT [dbo].[HoaDon] ([ID], [NgayDatHang], [HoTenKhach], [DiaChi], [DienThoai], [Email], [TongTien]) VALUES (1, CAST(N'2019-11-10 01:06:51.273' AS DateTime), N'gxxg', N'xgxgx', N'xvxvx', N'bxcxxcx', 16114000)
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
INSERT [dbo].[HoaDonChiTiet] ([HoaDonID], [SanPhamID], [SoLuong], [DonGia], [ThanhTien]) VALUES (1, 13, 4, 3900000, 15600000)
INSERT [dbo].[HoaDonChiTiet] ([HoaDonID], [SanPhamID], [SoLuong], [DonGia], [ThanhTien]) VALUES (1, 18, 5, 102800, 514000)
SET IDENTITY_INSERT [dbo].[Loai] ON 

INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (1, N'Bàn văn phòng', 1, N'ban-van-phong', CAST(500000 AS Numeric(18, 0)), N'23.jpg')
INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (2, N'Ghế văn phòng', 1, N'ghe-van-phong', CAST(500000 AS Numeric(18, 0)), N'22.jpg')
INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (3, N'Tủ Kệ văn phòng', 1, N'tu-ke-van-phong', CAST(500000 AS Numeric(18, 0)), N'21.jpg')
INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (4, N'Lavabo', 4, N'lavabo', CAST(500000 AS Numeric(18, 0)), N'2.jpg')
INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (5, N'Vòi Sen - vòi nước', 4, N'voi-sen-voi-nuoc', CAST(500000 AS Numeric(18, 0)), N'5.jpg')
INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (6, N'Bồn tắm', 4, N'bon-tam', CAST(500000 AS Numeric(18, 0)), N'19-11-06-12-42-00-15.jpg')
INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (7, N'Bàn Cafe', 2, N'ban-cafe', CAST(500000 AS Numeric(18, 0)), N'19-11-06-12-42-00-15.jpg')
INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (8, N'Sân thượngd', 8, N'san-thuongd', CAST(500000 AS Numeric(18, 0)), N'19-11-06-12-42-00-15.jpg')
INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (9, N'Casaredo1', 2, N'ghe-sofa', CAST(500000 AS Numeric(18, 0)), N'pro-big-2.jpg')
INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (10, N'Ghế thư giãn', 2, N'ghe-thu-gian', CAST(500000 AS Numeric(18, 0)), N'pro-big-1.jpg')
INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (11, N'Sofa Bed', 2, N'sofa-bed', CAST(500000 AS Numeric(18, 0)), N'pro-big-2.jpg')
INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (12, N'Kệ trang tri', 2, N'ke-trang-tri', CAST(500000 AS Numeric(18, 0)), N'pro-big-2.jpg')
INSERT [dbo].[Loai] ([LoaiID], [Ten], [ChungLoaiID], [BiDanh], [PriceFrom], [Hinh]) VALUES (13, N'Phụ kiện nhà tắm', 4, N'phu-kien-nha-tam', CAST(200000 AS Numeric(18, 0)), N'19-11-06-10-56-59-7.jpg')
SET IDENTITY_INSERT [dbo].[Loai] OFF
SET IDENTITY_INSERT [dbo].[NhaCungCap] ON 

INSERT [dbo].[NhaCungCap] ([NhaCungCapID], [Ten], [GioiThieu]) VALUES (1, N'Sorrento', N'Sorrento là dòng sản phẩm đồ gỗ nội thất làm từ gỗ công nghiệp cho thị trường trong và ngoài nước, cung cấp cho các dự án căn hộ cao cấp. Sorrento chú trọng vào thiết kế và sản xuất những sản phẩm nội thất chất lượng cao, tinh tế, sang trọng và hiện đại cho nhà ở và văn phòng. Sorrento có mạng lưới phủ rộng rãi tại Việt Nam và khắp khu vực Đông Nam Á như Singapore, Thái Lan, Malaysia.')
INSERT [dbo].[NhaCungCap] ([NhaCungCapID], [Ten], [GioiThieu]) VALUES (2, N'Kerasan', N'Kerasan là một trong những công ty hàng đầu sản xuất thiết bị vệ sinh sứ cao cấp tại Ý. Được thành lập vào năm 1960, Kerasan chuyên sản xuất và mang đến cho người tiêu dùng những sản phẩm sứ chất lượng cao. Trong nhiều năm qua, công ty không ngừng đánh dấu nhiều bước phát triển nổi trội, tập trung vào sự khéo léo tỉ mỉ của các nghệ nhân, các công nghệ và các nghiên cứu chuyên sâu về yếu tố thiết kế. Kerasan ngày cáng tạo dấu ấn tốt trên thị trường thế giới với các bộ sưu tập phòng tắm đa dạng từ phong cách cổ điển cho đến hiện đại.')
INSERT [dbo].[NhaCungCap] ([NhaCungCapID], [Ten], [GioiThieu]) VALUES (3, N'Hansgrohe', N'Hansgrohe được thành lập năm 1901 tại Black Forest, Đức. Mỗi sản phầm thiết bị vệ sinh của Hansgrohe không chỉ có chất lượng cao nhất mà còn là một tác phẩm nghệ thuật. Hansgrohe luôn đi trước các công ty khác trong việc phát minh ra các sản phẩm với mục đích đem lại những trải nghiệm thú vị nhất cho con người trong phòng tắm, đó có thể là phát minh tay sen và đầu sen đầu tiên tạo càm giác tắm mưa Riainace (2003).')
INSERT [dbo].[NhaCungCap] ([NhaCungCapID], [Ten], [GioiThieu]) VALUES (4, N'Dolce Rosa', N'Dolce Rosa là thương hiệu nội thất cao cấp đến từ Italy, với phong cách cổ điển kiểu Pháp, cùng với nét xa hoa lịch lãm đến từng chi tiết, và tông màu chủ đọa là màu trắng kem sẽ mang đến cho ngôi nhà của bạn trở nên đẳng cấp và ấn tượng.')
INSERT [dbo].[NhaCungCap] ([NhaCungCapID], [Ten], [GioiThieu]) VALUES (5, N'Valencasa', NULL)
INSERT [dbo].[NhaCungCap] ([NhaCungCapID], [Ten], [GioiThieu]) VALUES (6, N'SevenSedie', NULL)
INSERT [dbo].[NhaCungCap] ([NhaCungCapID], [Ten], [GioiThieu]) VALUES (7, N'Giorgio Collection', NULL)
INSERT [dbo].[NhaCungCap] ([NhaCungCapID], [Ten], [GioiThieu]) VALUES (9, N'Casaredo1', N'Casaredo1  là thương hiệu sofa cao cấp hàng đầu đến từ Malaysia, các mẫu sofa Casaredo được đánh giá cao về thiết kế kiểu dáng, cũng như các tiện ích liên quan đến nệm ngồi và tựa lưng. Toàn bộ da và vải làm nên mỗi chiếc Sofa đều được nhập khẩu hoàn toàn từ Italia. Thế Giới Nội Thất tự hào là nhà phân phối độc quyền sản phẩm của Casaredo.')
SET IDENTITY_INSERT [dbo].[NhaCungCap] OFF
SET IDENTITY_INSERT [dbo].[SanPham] ON 

INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (5, N'A42490', N'Móc treo Kerasan', N'', 4700, 2, N'Italia', 4, N'Sứ, kim loại mạ crom', N'Trắng, bạc crom', N'5.jpg', 0, NULL, 0, N'22.jpg,22.jpg,21.jpg,22.jpg', 5)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (6, N'A42190', N'Giá treo khăn giấy Kerasan', N'', 6500, 2, N'Italia', 4, N'Sứ, kim loại mạ crom', N'Trắng, bạc crom', N'6.jpg', 0, NULL, 1, N'22.jpg,22.jpg,21.jpg,22.jpg', 10)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (7, N'A40834', N'Ly để bàn chải Hansgrohe Starck ', N'', 6600, 3, N'Đức', 4, N'Đồng mạ chrome', N'Bạc', N'7.jpg', 0, NULL, 1, N'22.jpg,22.jpg,21.jpg,22.jpg', NULL)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (8, N'A10651', N'Bộ tay sen Hansgrohe Shower Collection ', N'Giới thiệu sản phẩm:

Tay sen là một trong những bộ phận quan trọng của bộ vòi sen vì vậy nó được đòi hỏi phải có chất lượng cao, giá thành tương đối và mẫu mã đẹp mắt. Thế nên, bạn hãy thử tham khảo Bộ tay sen Shower Collection.

Tiện ích nổi bật:

Bộ tay sen Shower Collection sử dụng đồng làm chất liệu gia công chính, bên ngoài mạ chrome để tạo sự đẹp mắt, bóng bẩy sang trọng cho sản phẩm. Bộ tay sen này có phần bệ đỡ với 2 mảnh chữ nhật ghép lại chắc chắn, trên đó là núm vặn để điều khiển và bộ dây ống nước được nối thẳng vào bệ đỡ này.

Bài trí không gian:', 22000, 3, N'Đức', 4, N'Lõi đồng mạ chrome', N'Bạc', N'8.jpg', 0, NULL, 2, N'22.jpg,22.jpg,21.jpg,22.jpg', 10)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (9, N'A31608', N'Vòi nước Lavabo mạ Crom Hansgrohe Focus ', N'Với chất liệu làm từ lõi đồng mạ Crom và khả năng ép không khí vào trong, đẩy nước ra ngoài theo dạng bọt nên không gây rát da khi sử dụng, thể hiện tính hiện đại và sang trọng cho không gian của bạn.', 5500, 3, N'Đức', 4, N'Lõi đồng mạ chrome', N'Crom bạc', N'9.jpg', 0, NULL, 3, N'22.jpg,22.jpg,21.jpg,22.jpg', 50)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (10, N'A140K1', N'Bộ lavabo Waldorf Kerasan', NULL, 25000, 2, N'Italia', 4, N'Sứ', N'Trắng', N'10.jpg', 0, NULL, 2, N'22.jpg,22.jpg,21.jpg,22.jpg', NULL)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (11, N'AQ638L', N'Bàn cafe tròn khung inox mặt gỗ MDF giả đá Sorrento', N'Bàn cafe AQ638N của Sorrento là một thiết kế độc đáo dành cho những khách hàng muốn tạo sự phá cách trong cách bài trí không gian phòng khách.

Tiện ích nổi bật:

Được làm từ gỗ MDF và inox, màu trắng kem sẽ là sự lựa chọn cho bạn để có thể dễ dàng phối hợp với các nội thất khác.

Bài trí không gian:

Với thiết kế lạ mắt và khá thú vị, chiếc bàn cafe này mang lại sự lý thú cho người sở hữu cũng như người sử dụng.', 12300, 1, N'Việt Nam', 7, N'chân inox, mặt bàn gỗ MDF công nghệ sơn đá marble', N'Nâu, kem', N'11.jpg', 0, NULL, 2, N'22.jpg,22.jpg,21.jpg,22.jpg', NULL)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (12, N'AQR703', N'Bàn cà phê Dolce Rosa', NULL, 21800, 4, N'Malaysia', 7, N'Gỗ sồi, kính', N'Trắng kem', N'12.jpg', 0, NULL, 2, N'22.jpg,22.jpg,21.jpg,22.jpg', NULL)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (13, N'AMT205', N'Bàn cà phê Sorrento', N'Với chất liệu làm từ lõi đồng mạ Crom và khả năng ép không khí vào trong, đẩy nước ra ngoài theo dạng bọt nên không gây rát da khi sử dụng, thể hiện tính hiện đại và sang trọng cho không gian của bạn.', 3900000, 1, N'Việt Nam', 7, N'Kính, sắt sơn', N'Mặt bàn màu trắng xanh, chân bàn màu xám', N'13.jpg', 0, NULL, 4, N'22.jpg,22.jpg,21.jpg,22.jpg', 5)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (14, N'AQR705', N'Bàn cà phê cao cấp ', NULL, 4000, 1, N'Thái Lan', 7, N'Khung hợp kim, bề mặt bọc da', N'Đồng, đen', N'14.jpg', 0, NULL, 3, N'22.jpg,22.jpg,21.jpg,22.jpg', 69)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (15, N'AQR706', N'Bàn cà phê Sorrento', NULL, 3900, 1, N'Việt Nam', 7, N'Kính, inox', NULL, N'15.jpg', 0, NULL, 2, N'22.jpg,22.jpg,21.jpg,22.jpg', NULL)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (16, N'AQR707', N'Bàn cafe mặt bọc da cao cấp', NULL, 12000, 1, N'Thái Lan', 7, N'Khung hợp kim, bề mặt bọc da', N'Đồng, đen', N'16.jpg', 0, NULL, 4, N'22.jpg,22.jpg,21.jpg,22.jpg', 55)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (17, N'AC2093', N'Bàn cafe Valencasa gỗ sồi hiện đại', N'Giới thiệu sản phẩm:
Không gian phòng khách của bạn sẽ trở nên vô cùng bắt mắt với chiếc bàn cà phê Valencasa này, mang phong cách hiện đại cùng những đường nét nhấn nhá ở phần chân bàn sẽ tạo nét độc đáo riêng cho căn phòng khách.', 21900, 5, N'Malaysia', 7, N'Gỗ sồi', N'Nâu', N'17.jpg', 0, NULL, 1, N'22.jpg,22.jpg,21.jpg,22.jpg', 88)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (18, N'A0TA98', N'Bàn SevenSedie Sigmund khắc hoa văn viền đen ', NULL, 102800, 6, N'Italia', 7, N'Gỗ Beech', N'Nâu, đen', N'18.jpg', 0, NULL, 1, N'22.jpg,22.jpg,21.jpg,22.jpg', 77)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (19, N'BGTG04', N'Ghế thư giãn gỗ Valencasa màng vải nâu ', NULL, 5000, 5, N'Malaysia', 10, N'Gỗ', N'Nâu', N'19.jpg', 0, NULL, 3, N'22.jpg,22.jpg,21.jpg,22.jpg', NULL)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (20, N'AA900G', N'Ghế thư giãn Giorgio Neon phong cách hiện đại ', NULL, 12000, 7, N'Italia', 10, N'Inox, vải', N'Vàng', N'20.jpg', 0, NULL, 4, N'22.jpg,22.jpg,21.jpg,22.jpg', NULL)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (21, N'AA800G', N'Ghế xoay thư giãn Giorgio mang nét năng động', NULL, 12000, 7, N'Italia', 10, N'Inox, vải', N'Vàng', N'21.jpg', 0, NULL, 2, N'22.jpg,22.jpg,21.jpg,22.jpg', 55)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (22, N'A0493A', N'Ghế bành nhỏ Sevensedie FEEL khung gỗ bọc vải trắng', NULL, 12000, 6, N'Italia', 10, N'Gỗ Beech, vải', N'Nâu, trắng', N'22.jpg', 0, NULL, 4, N'22.jpg,22.jpg,21.jpg,22.jpg', NULL)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (23, N'A6227G', N'Sofa góc Casaredo trắng kem vải nhung hiện đại ', NULL, 18000, 9, N'Malaysia', 9, N'Vải', N'Kem', N'23.jpg', 0, NULL, 3, N'22.jpg,22.jpg,21.jpg,22.jpg', 55)
INSERT [dbo].[SanPham] ([SanPhamID], [MaSo], [Ten], [MoTa], [GiaBan], [NhaCungCapID], [XuatXu], [LoaiID], [ChatLieu], [MauSac], [Hinh], [HetHang], [BiDanh], [star], [ListHinh], [GiamGia]) VALUES (24, N'AV618F', N'Ghế xoay văn phòng Safari trẻ trung', NULL, 9000, 9, N'Malaysia', 2, N'Inox, vải, da', N'Cam', N'24.jpg', 0, NULL, 0, N'19-11-05-11-44-32-19.jpg,19-11-05-11-44-32-20.jpg,19-11-05-11-44-32-23.jpg,19-11-05-11-44-32-24.jpg', NULL)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
ALTER TABLE [dbo].[HoaDonChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonChiTiet_HoaDon] FOREIGN KEY([HoaDonID])
REFERENCES [dbo].[HoaDon] ([ID])
GO
ALTER TABLE [dbo].[HoaDonChiTiet] CHECK CONSTRAINT [FK_HoaDonChiTiet_HoaDon]
GO
ALTER TABLE [dbo].[HoaDonChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonChiTiet_SanPham] FOREIGN KEY([SanPhamID])
REFERENCES [dbo].[SanPham] ([SanPhamID])
GO
ALTER TABLE [dbo].[HoaDonChiTiet] CHECK CONSTRAINT [FK_HoaDonChiTiet_SanPham]
GO
ALTER TABLE [dbo].[Loai]  WITH CHECK ADD  CONSTRAINT [FK_Loai_ChungLoai] FOREIGN KEY([ChungLoaiID])
REFERENCES [dbo].[ChungLoai] ([ChungLoaiID])
GO
ALTER TABLE [dbo].[Loai] CHECK CONSTRAINT [FK_Loai_ChungLoai]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_Loai] FOREIGN KEY([LoaiID])
REFERENCES [dbo].[Loai] ([LoaiID])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_Loai]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_NhaCungCap] FOREIGN KEY([NhaCungCapID])
REFERENCES [dbo].[NhaCungCap] ([NhaCungCapID])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_NhaCungCap]
GO
USE [master]
GO
ALTER DATABASE [NoiThat] SET  READ_WRITE 
GO
