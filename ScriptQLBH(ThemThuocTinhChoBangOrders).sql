/*
Navicat SQL Server Data Transfer

Source Server         : SQLServer
Source Server Version : 120000
Source Host           : .\SQLEXPRESS:1433
Source Database       : QLBH
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 120000
File Encoding         : 65001

Date: 2017-06-09 00:24:03
*/


-- ----------------------------
-- Table structure for Categories
-- ----------------------------
DROP TABLE [dbo].[Categories]
GO
CREATE TABLE [dbo].[Categories] (
[CatID] int NOT NULL IDENTITY(1,1) ,
[CatName] nvarchar(50) NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Categories]', RESEED, 33)
GO

-- ----------------------------
-- Records of Categories
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Categories] ON
GO
INSERT INTO [dbo].[Categories] ([CatID], [CatName]) VALUES (N'1', N'Sách - Truyện')
GO
GO
INSERT INTO [dbo].[Categories] ([CatID], [CatName]) VALUES (N'2', N'Điện thoại')
GO
GO
INSERT INTO [dbo].[Categories] ([CatID], [CatName]) VALUES (N'3', N'Máy chụp hình')
GO
GO
INSERT INTO [dbo].[Categories] ([CatID], [CatName]) VALUES (N'4', N'Quần áo - Giày dép')
GO
GO
INSERT INTO [dbo].[Categories] ([CatID], [CatName]) VALUES (N'6', N'Đồ trang sức')
GO
GO
INSERT INTO [dbo].[Categories] ([CatID], [CatName]) VALUES (N'7', N'Khác')
GO
GO
INSERT INTO [dbo].[Categories] ([CatID], [CatName]) VALUES (N'30', N'a')
GO
GO
INSERT INTO [dbo].[Categories] ([CatID], [CatName]) VALUES (N'31', N'Đồng hồ')
GO
GO
INSERT INTO [dbo].[Categories] ([CatID], [CatName]) VALUES (N'32', N'b')
GO
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO

-- ----------------------------
-- Table structure for NhaSanXuat
-- ----------------------------
DROP TABLE [dbo].[NhaSanXuat]
GO
CREATE TABLE [dbo].[NhaSanXuat] (
[IDNhaSanXuat] int NOT NULL IDENTITY(1,1) ,
[TenNhaSanXuat] nvarchar(50) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[NhaSanXuat]', RESEED, 21)
GO

-- ----------------------------
-- Records of NhaSanXuat
-- ----------------------------
SET IDENTITY_INSERT [dbo].[NhaSanXuat] ON
GO
INSERT INTO [dbo].[NhaSanXuat] ([IDNhaSanXuat], [TenNhaSanXuat]) VALUES (N'1', N'Hưng Thịnh')
GO
GO
INSERT INTO [dbo].[NhaSanXuat] ([IDNhaSanXuat], [TenNhaSanXuat]) VALUES (N'2', N'Hoàng Kim')
GO
GO
INSERT INTO [dbo].[NhaSanXuat] ([IDNhaSanXuat], [TenNhaSanXuat]) VALUES (N'3', N'PNJ')
GO
GO
INSERT INTO [dbo].[NhaSanXuat] ([IDNhaSanXuat], [TenNhaSanXuat]) VALUES (N'4', N'Samsung')
GO
GO
INSERT INTO [dbo].[NhaSanXuat] ([IDNhaSanXuat], [TenNhaSanXuat]) VALUES (N'5', N'NEW')
GO
GO
INSERT INTO [dbo].[NhaSanXuat] ([IDNhaSanXuat], [TenNhaSanXuat]) VALUES (N'6', N'ZARA')
GO
GO
INSERT INTO [dbo].[NhaSanXuat] ([IDNhaSanXuat], [TenNhaSanXuat]) VALUES (N'7', N'Tiki')
GO
GO
INSERT INTO [dbo].[NhaSanXuat] ([IDNhaSanXuat], [TenNhaSanXuat]) VALUES (N'8', N'Canon')
GO
GO
INSERT INTO [dbo].[NhaSanXuat] ([IDNhaSanXuat], [TenNhaSanXuat]) VALUES (N'9', N'Sony')
GO
GO
INSERT INTO [dbo].[NhaSanXuat] ([IDNhaSanXuat], [TenNhaSanXuat]) VALUES (N'20', N'a')
GO
GO
SET IDENTITY_INSERT [dbo].[NhaSanXuat] OFF
GO

-- ----------------------------
-- Table structure for OrderDetails
-- ----------------------------
DROP TABLE [dbo].[OrderDetails]
GO
CREATE TABLE [dbo].[OrderDetails] (
[OrderID] int NOT NULL ,
[ProID] int NOT NULL ,
[Quantity] int NOT NULL ,
[Price] money NOT NULL ,
[Amount] money NOT NULL 
)


GO

-- ----------------------------
-- Records of OrderDetails
-- ----------------------------
INSERT INTO [dbo].[OrderDetails] ([OrderID], [ProID], [Quantity], [Price], [Amount]) VALUES (N'1', N'1', N'2', N'1500000.0000', N'3000000.0000')
GO
GO
INSERT INTO [dbo].[OrderDetails] ([OrderID], [ProID], [Quantity], [Price], [Amount]) VALUES (N'1', N'2', N'2', N'300000.0000', N'600000.0000')
GO
GO
INSERT INTO [dbo].[OrderDetails] ([OrderID], [ProID], [Quantity], [Price], [Amount]) VALUES (N'2', N'1', N'1', N'1500000.0000', N'1500000.0000')
GO
GO
INSERT INTO [dbo].[OrderDetails] ([OrderID], [ProID], [Quantity], [Price], [Amount]) VALUES (N'2', N'2', N'1', N'300000.0000', N'300000.0000')
GO
GO
INSERT INTO [dbo].[OrderDetails] ([OrderID], [ProID], [Quantity], [Price], [Amount]) VALUES (N'3', N'29', N'1', N'500000000.0000', N'500000000.0000')
GO
GO
INSERT INTO [dbo].[OrderDetails] ([OrderID], [ProID], [Quantity], [Price], [Amount]) VALUES (N'4', N'1', N'1', N'1500000.0000', N'1500000.0000')
GO
GO
INSERT INTO [dbo].[OrderDetails] ([OrderID], [ProID], [Quantity], [Price], [Amount]) VALUES (N'5', N'53', N'1', N'11900000.0000', N'11900000.0000')
GO
GO
INSERT INTO [dbo].[OrderDetails] ([OrderID], [ProID], [Quantity], [Price], [Amount]) VALUES (N'6', N'1', N'2', N'1500000.0000', N'3000000.0000')
GO
GO
INSERT INTO [dbo].[OrderDetails] ([OrderID], [ProID], [Quantity], [Price], [Amount]) VALUES (N'7', N'45', N'1', N'64000.0000', N'64000.0000')
GO
GO
INSERT INTO [dbo].[OrderDetails] ([OrderID], [ProID], [Quantity], [Price], [Amount]) VALUES (N'8', N'45', N'1', N'64000.0000', N'64000.0000')
GO
GO

-- ----------------------------
-- Table structure for Orders
-- ----------------------------
DROP TABLE [dbo].[Orders]
GO
CREATE TABLE [dbo].[Orders] (
[OrderID] int NOT NULL IDENTITY(1,1) ,
[OrderDate] datetime NOT NULL ,
[UserID] int NOT NULL ,
[Total] money NOT NULL ,
[TinhTrangGiao] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Orders]', RESEED, 8)
GO

-- ----------------------------
-- Records of Orders
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Orders] ON
GO
INSERT INTO [dbo].[Orders] ([OrderID], [OrderDate], [UserID], [Total], [TinhTrangGiao]) VALUES (N'1', N'2014-03-14 00:00:00.000', N'5', N'3600000.0000', N'0')
GO
GO
INSERT INTO [dbo].[Orders] ([OrderID], [OrderDate], [UserID], [Total], [TinhTrangGiao]) VALUES (N'2', N'2014-03-14 00:00:00.000', N'5', N'1800000.0000', N'0')
GO
GO
INSERT INTO [dbo].[Orders] ([OrderID], [OrderDate], [UserID], [Total], [TinhTrangGiao]) VALUES (N'3', N'2017-05-24 23:11:05.413', N'1021', N'500000000.0000', N'1')
GO
GO
INSERT INTO [dbo].[Orders] ([OrderID], [OrderDate], [UserID], [Total], [TinhTrangGiao]) VALUES (N'4', N'2017-05-25 08:14:12.283', N'1021', N'1500000.0000', N'1')
GO
GO
INSERT INTO [dbo].[Orders] ([OrderID], [OrderDate], [UserID], [Total], [TinhTrangGiao]) VALUES (N'5', N'2017-05-25 20:33:45.613', N'9', N'11900000.0000', N'0')
GO
GO
INSERT INTO [dbo].[Orders] ([OrderID], [OrderDate], [UserID], [Total], [TinhTrangGiao]) VALUES (N'6', N'2017-05-25 20:35:02.520', N'9', N'3000000.0000', N'0')
GO
GO
INSERT INTO [dbo].[Orders] ([OrderID], [OrderDate], [UserID], [Total], [TinhTrangGiao]) VALUES (N'7', N'2017-05-25 20:35:59.350', N'9', N'64000.0000', N'0')
GO
GO
INSERT INTO [dbo].[Orders] ([OrderID], [OrderDate], [UserID], [Total], [TinhTrangGiao]) VALUES (N'8', N'2017-06-02 09:41:05.887', N'1021', N'64000.0000', N'1')
GO
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO

-- ----------------------------
-- Table structure for Products
-- ----------------------------
DROP TABLE [dbo].[Products]
GO
CREATE TABLE [dbo].[Products] (
[ProID] int NOT NULL IDENTITY(1,1) ,
[ProName] nvarchar(40) NOT NULL ,
[TinyDes] nvarchar(80) NOT NULL ,
[FullDes] ntext NOT NULL ,
[Price] money NOT NULL ,
[CatID] int NOT NULL ,
[Quantity] int NOT NULL DEFAULT ((0)) ,
[NgayNhap] datetime NOT NULL ,
[SoLuotXem] int NULL ,
[XuatXu] nvarchar(50) NULL ,
[Loai] int NULL ,
[IDNhaSanXuat] int NULL ,
[BiXoa] tinyint NULL ,
[SoLuongDaBan] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Products]', RESEED, 110)
GO

-- ----------------------------
-- Records of Products
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Products] ON
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'1', N'Áo phông nam Gos', N'Thanh lịch, mạnh mẽ', N'<UL>
    <LI>Vải: cotton </LI>
    <LI>Màu: xanh dương đậm</LI>
    <LI>Size: M - L - XL</LI>
    <LI>Dài áo: 7.5 inches</LI>
</UL>', N'1500000.0000', N'4', N'89', N'2017-05-09 00:24:47.000', N'10', N'Việt Nam', N'1', N'1', N'0', N'20')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'2', N'Nhẫn kim cương bạch kim', N'Sang trọng, quý phái', N'<P><STRONG>Thông tin viên kim cương</STRONG></P>
<UL>
    <LI>Nhập khẩu từ: Dubai</LI>
</UL>', N'300000.0000', N'6', N'64', N'2017-05-19 00:25:02.000', N'43', N'Việt Nam', N'2', N'2', N'0', N'99')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'3', N'Vòng đeo tay tình nhân', N'Thể hiện tình yêu đôi lứa', N'<UL>
    <LI>Kiểu sản phẩm: Vòng đeo tay</LI>
    <LI>Chất liệu: Bạc</LI>
    <LI>Điểm tô cho tình yêu của bạn</LI>
</UL>
', N'1600000000.0000', N'6', N'86', N'2017-05-01 00:25:07.000', N'101', N'Việt Nam', N'1', N'2', N'0', N'67')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'4', N'Lắc tay cặp PNJ', N'Tinh tế, sắc nét', N'<UL>
    <LI>Kiểu sản phẩm: Lắc tay cặp</LI>
    <LI>Chất liệu: Inox cao cấp, không gây kích ứng da</LI>
    <LI>Mẫu mã: Tinh tế, sắc nét</LI>
    <LI>Lắc tay nam nặng: 21 gram</LI>
    <LI>Lắc tay nữ nặng: 11 gram</LI>
</UL>', N'42000000.0000', N'6', N'63', N'2017-05-30 00:25:12.000', N'107', N'Việt Nam', N'1', N'3', N'0', N'42')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'5', N'Điện thoại Samsung Galaxy Note 5', N'Camera: 16MP Pin: 3000 mAh', N'<UL>
    <LI>Màn hình: 5.7", Quad HD</LI>
    <LI>Hệ điều hành: Android 6.0</LI>
    <LI>CPU: 8 nhân</LI>
    <LI>RAM: 4 GB</LI>
    <LI>ROM: 32 GB</LI>
    <LI>Camera: 16 MP</LI>
    <LI>SIM: 1 Sim</LI>
    <LI>Pin: 3000 mAh, có sạc nhanh</LI>
</UL>

', N'12900000.0000', N'2', N'1', N'2016-08-20 00:25:17.000', N'14', N'Hàn Quốc', N'1', N'4', N'0', N'5')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'6', N'Áo thun nam Hollister', N'Màu sắc tươi tắn, kiểu dáng trẻ trung', N'<UL>
    <LI>Loại hàng: Hàng trong nước</LI>
    <LI>Xuất xứ: Tp Hồ Chí Minh</LI>
</UL>
', N'180000.0000', N'4', N'62', N'2017-05-18 00:00:00.000', N'0', N'Việt Nam', N'3', N'1', N'0', N'0')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'7', N'Điện thoại Samsung Galaxy S8 Plus', N'Thiết kế tinh xảo, hiện đại', N'<UL>
    <LI>Màn hình: 6.2", Quad HD</LI>
    <LI>Hệ điều hành: Android 7.0</LI>
    <LI>CPU: 8 nhân</LI>
    <LI>RAM: 4 GB</LI>
    <LI>ROM: 64 GB</LI>
    <LI>Camera: 12 MP</LI>
    <LI>SIM: 2 Sim</LI>
    <LI>Pin: 3500 mAh, có sạc nhanh</LI>
</UL>

', N'29990000.0000', N'2', N'15', N'2017-01-09 00:25:32.000', N'53', N'Hàn Quốc', N'1', N'4', N'0', N'89')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'8', N'Áo bé trai Bibos', N'Hóm hỉnh dễ thương', N'<UL>
    <LI>Quần áo bé trai</LI>
    <LI>Loại hàng: Hàng trong nước</LI>
    <LI>Xuất xứ: Tp Hồ Chí Minh</LI>
</UL>
', N'270000.0000', N'4', N'74', N'2016-11-26 00:25:41.000', N'6', N'Việt Nam', N'3', N'1', N'0', N'25')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'9', N'Bông tai bạc đính đá', N'Trẻ trung và quý phái', N'<UL>
    <LI>Tên sản phẩm: Bông tai bạc đính đá</LI>
    <LI>Đóng nhãn hiệu: Torrini</LI>
    <LI>Nguồn gốc, xuất xứ: Italy</LI>
    <LI>Hình thức thanh toán: L/C T/T M/T CASH</LI>
    <LI>Thời gian giao hàng: trong vòng 3 ngày kể từ ngày mua</LI>
    <LI>Chất lượng/chứng chỉ: good</LI>
</UL>
', N'2400000.0000', N'6', N'43', N'2017-02-28 00:25:53.000', N'91', N'Việt Nam', N'2', N'2', N'0', N'26')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'10', N'Đầm chấm bi bé gái', N'Kiểu dáng xinh xắn', N'<UL>
    <LI>Màu sắc: vàng, hồng, xanh biển.</LI>
    <LI>Kiểu dáng: đầm xòe tay cánh tiên xếp ly</LI>
    <LI>Vải: cotton</LI>
</UL>', N'2800000.0000', N'4', N'80', N'2017-05-19 00:26:06.000', N'79', N'Thái Lan', N'2', N'5', N'0', N'19')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'11', N'Dây chuyền 24k', N'Kiểu dáng hoa mai may mắn', N'<UL>
    <LI>Chất liệu chính: Vàng 24k</LI>
    <LI>Màu sắc: Vàng</LI>
    <LI>Chất lượng: Mới</LI>
    <LI>Phí vận chuyển: Liên hệ</LI>
    <LI>Giá bán có thể thay đổi tùy theo trọng lượng và giá vàng của từng thời điểm.</LI>
</UL>

', N'2500000.0000', N'6', N'88', N'2017-05-27 00:26:11.000', N'203', N'Hoa Kỳ', N'2', N'2', N'0', N'5')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'12', N'Đồ bộ bé trai', N'Đủ màu sắc - kiểu dáng', N'<UL>
    <LI>Màu sắc: đỏ tía, xanh biển, vàng tím, trắng và đen.</LI>
    <LI>Xuất xứ: Tp. Hồ Chí Minh</LI>
    <LI>Chất liệu: cotton</LI>
    <LI>Loại hàng: hàng trong nước</LI>
</UL>
', N'120000.0000', N'4', N'61', N'2017-03-10 00:26:19.000', N'55', N'Thái Lan', N'3', N'1', N'0', N'80')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'13', N'Đầm công sở IRis', N'Đơn giản nhưng quý phái', N'<P>Những đường cong tuyệt đẹp sẽ càng được phô bày khi diện các thiết kế này.</P>
<UL>
    <LI>Nét cắt táo bạo ở ngực giúp bạn gái thêm phần quyến rũ, ngay cả khi không có trang&nbsp; sức nào trên người.</LI>
    <LI>Đầm hai dây thật điệu đà với nơ xinh trước ngực nhưng trông bạn vẫn toát lên vẻ tinh nghịch và bụi bặm nhờ thiết kế đầm bí độc đáo cùng sắc màu sẫm.</LI>
    <LI>Hãng sản xuất: NEW</LI>
    <LI>Kích cỡ : Tất cả các kích cỡ</LI>
    <LI>Kiểu dáng : Quây/Ống</LI>
    <LI>Chất liệu : Satin</LI>
    <LI>Màu : đen, đỏ</LI>
    <LI>Xuất xứ : Việt Nam</LI>
</UL>
', N'2600000.0000', N'4', N'92', N'2017-05-18 00:26:36.000', N'82', N'Việt Nam', N'2', N'5', N'0', N'2')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'14', N'Đầm xòe lá sen', N'Táo bạo và quyến rũ', N'<P>Những đường cong tuyệt đẹp sẽ càng được phô bày khi diện các thiết kế này.</P>
<UL>
    <LI>Nét cắt táo bạo ở ngực giúp bạn gái thêm phần quyến rũ, ngay cả khi không có trang&nbsp; sức nào trên người.</LI>
    <LI>Đầm hai dây thật điệu đà với nơ xinh trước ngực nhưng trông bạn vẫn toát lên vẻ tinh nghịch và bụi bặm nhờ thiết kế đầm bí độc đáo cùng sắc màu sẫm.</LI>
    <LI>Hãng sản xuất: NEW</LI>
    <LI>Kích cỡ : Tất cả các kích cỡ</LI>
    <LI>Kiểu dáng : Quây/Ống</LI>
    <LI>Chất liệu : Satin</LI>
    <LI>Màu : đen, đỏ</LI>
    <LI>Xuất xứ : Việt Nam</LI>
</UL>
', N'1200000.0000', N'4', N'0', N'2017-04-21 00:26:40.000', N'5', N'Việt Nam', N'2', N'5', N'0', N'0')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'15', N'Dây chuyền đá quý', N'Kết hợp vàng trắng và đá quý', N'<UL>
    <LI>Kiểu sản phẩm: Dây chuyền</LI>
    <LI>Chất liệu: Vàng trắng 14K và đá quý, nguyên liệu và công nghệ Italy...</LI>
    <LI>Trọng lượng chất liệu: 1.1 Chỉ </LI>
</UL>
', N'1925000.0000', N'6', N'22', N'2017-05-18 00:26:47.000', N'4', N'Hoa Kỳ', N'2', N'2', N'0', N'9')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'16', N'Điện thoại Samsung Galaxy A9 Pro', N'Pin siêu cực khủng', N'<UL>
    <LI>Màn hình: 6.0"</LI>
    <LI>Màu sắc: Gold/White/Black</LI>
    <LI>Sim: 2 Sim</LI>
    <LI>Pin: 2800 mAh</LI>
    <LI>Hệ điều hành: Android 5.2</LI>
</UL>
', N'290000.0000', N'2', N'81', N'2017-01-26 00:26:51.000', N'0', N'Hàn Quốc', N'3', N'4', N'0', N'6')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'17', N'Dây chuyền phượng hoàng', N'Thể hiện đẳng cấp', N'<UL>
    <LI>Kiểu sản phẩm:&nbsp; Mặt dây</LI>
    <LI>Chất liệu: Vàng trắng 14K, nguyên liệu và công nghệ Italy...</LI>
    <LI>Trọng lượng chất liệu: 0.32 Chỉ</LI>
</UL>
', N'1820000.0000', N'6', N'33', N'2017-05-12 00:26:58.000', N'4', N'Việt Nam', N'2', N'2', N'0', N'8')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'18', N'Vòng đeo tay đá mắt hổ', N'Sang trọng, quý phái', N'<UL>
    <LI>Kiểu sản phẩm: Vòng đeo tay</LI>
    <LI>Chất liệu: Vàng trắng 14K và đá quý, nguyên liệu và công nghệ Italy...</LI>
    <LI>Trọng lượng chất liệu: 1.1 Chỉ </LI>
</UL>
', N'3400000.0000', N'6', N'10', N'2017-04-26 00:27:02.000', N'7', N'Việt Nam', N'2', N'2', N'0', N'10')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'19', N'Vòng đeo tay Gamulet', N'Đơn giản, thanh lịch', N'<UL>
    <LI>Kiểu sản phẩm:&nbsp; Vòng đeo tay</LI>
    <LI>Chất liệu: Bạc 18K, nguyên liệu và công nghệ Italy...</LI>
    <LI>Trọng lượng chất liệu: 1 Chỉ</LI>
</UL>
', N'1820000.0000', N'6', N'17', N'2017-05-09 00:27:06.000', N'9', N'Việt Nam', N'2', N'2', N'0', N'10')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'20', N'Giày nam G9', N'Êm - đẹp - bền', N'<UL>
    <LI>Loại hàng: Hàng trong nước</LI>
    <LI>Xuất xứ: Tp Hồ Chí Minh</LI>
</UL>
', N'5400000.0000', N'4', N'0', N'2017-02-16 00:27:15.000', N'8', N'Việt Nam', N'2', N'6', N'0', N'9')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'21', N'Giày nam G3', N'Đen bóng, sang trọng', N'<UL>
    <LI>Loại hàng: Hàng trong nước</LI>
    <LI>Xuất xứ: Tp Hồ Chí Minh</LI>
</UL>
', N'300000.0000', N'4', N'74', N'2017-05-25 00:27:27.000', N'38', N'Việt Nam', N'3', N'6', N'0', N'29')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'22', N'Giày nữ HH7083', N'Kiểu dáng thanh thoát', N'<UL>
    <LI>Loại hàng: Hàng trong nước</LI>
    <LI>Xuất xứ: Tp Hồ Chí Minh</LI>
</UL>
', N'290000.0000', N'4', N'30', N'2017-05-20 00:27:33.000', N'70', N'Việt Nam', N'3', N'6', N'0', N'19')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'23', N'Nhẫn PNJ 416', N'Kiểu dáng độc đáo', N'<P><STRONG>Thông tin sản phẩm</STRONG></P>
<UL>
    <LI>Mã sản phẩm: PNJ 416</LI>
    <LI>Trọng lượng: 2 chỉ</LI>
    <LI>Vật liệu chính: Bạc 24K</LI>
</UL>
', N'36000000.0000', N'6', N'5', N'2017-05-26 00:27:37.000', N'90', N'Việt Nam', N'1', N'3', N'0', N'35')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'24', N'Nhẫn Versace', N'Sáng chói - mới lạ', N'<P><STRONG>Thông tin sản phẩm</STRONG></P>
<UL>
    <LI>Mã sản phẩm: V005</LI>
    <LI>Trọng lượng: 1 cây</LI>
    <LI>Vật liệu chính: Vàng 24K</LI>
</UL>
', N'14900000.0000', N'6', N'22', N'2017-05-10 00:27:40.000', N'86', N'Hoa Kỳ', N'1', N'2', N'0', N'39')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'25', N'Nhẫn C585', N'Độc đáo, sang trọng', N'<UL>
    <LI>Kiểu dáng nam tính và độc đáo, những thiết kế dưới đây đáp ứng được mọi yêu cần khó tính nhất của người sở hữu.</LI>
    <LI>Những hạt kim cương sẽ giúp người đeo nó tăng thêm phần sành điệu</LI>
    <LI>Không chỉ có kiểu dáng truyền thống chỉ có một hạt kim cương ở giữa, các nhà thiết kế đã tạo những những chiếc nhẫn vô cùng độc đáo và tinh tế.</LI>
    <LI>Tuy nhiên, giá của đồ trang sức này thì chỉ có dân chơi mới có thể kham được</LI>
</UL>
', N'2400000000.0000', N'6', N'52', N'2017-05-01 00:27:45.000', N'56', N'Việt Nam', N'1', N'2', N'0', N'18')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'26', N'Nhẫn C900', N'Nữ tính - đầy quý phái', N'<UL>
    <LI>Để sở hữu một chiếc nhẫn kim cương lấp lánh trên tay, bạn phải là người chịu chi và sành điệu.</LI>
    <LI>Với sự kết hợp khéo léo và độc đáo giữa kim cương và Saphia, Ruby... những chiếc nhẫn càng trở nên giá trị</LI>
    <LI>Nhà sản xuất: Torrini</LI>
</UL>
<P>Cái này rất phù hợp cho bạn khi tặng nàng</P>
', N'19500000.0000', N'6', N'11', N'2017-05-16 00:27:48.000', N'89', N'Việt Nam', N'1', N'2', N'0', N'20')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'27', N'Nhẫn D920', N'Sự kết hợp khéo léo, độc đáo', N'<UL>
    <LI>Để sở hữu một chiếc nhẫn kim cương lấp lánh trên tay, bạn phải là người chịu chi và sành điệu.</LI>
    <LI>Với sự kết hợp khéo léo và độc đáo giữa kim cương và Saphia, Ruby... những chiếc nhẫn càng trở nên giá trị</LI>
    <LI>Nhà sản xuất: Torrini</LI>
</UL>
<P>Cái này rất phù hợp cho bạn khi tặng nàng</P>
', N'3100000000.0000', N'6', N'28', N'2017-05-29 00:27:52.000', N'27', N'Việt Nam', N'1', N'2', N'0', N'11')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'28', N'Nhẫn C892', N'Tinh xảo - sang trọng', N'<UL>
    <LI>Kim cương luôn là đồ trang sức thể hiện đẳng cấp của người sử dụng.</LI>
    <LI>Không phải nói nhiều về những kiểu nhẫn dưới đây, chỉ có thể gói gọn trong cụm từ: tinh xảo và sang trọng</LI>
    <LI>Thông tin nhà sản xuất: Torrini</LI>
    <LI>Thông tin chi tiết: Cái này rất phù hợp cho bạn khi tặng nàng</LI>
</UL>
', N'1800000000.0000', N'6', N'29', N'2017-05-25 00:27:58.000', N'19', N'Việt Nam', N'1', N'2', N'0', N'10')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'29', N'Nhẫn T900', N'Tinh tế đến không ngờ', N'<UL>
    <LI>Tinh xảo và sang trọng</LI>
    <LI>Thông tin nhà sản xuất: Torrini</LI>
    <LI>Không chỉ có kiểu dáng truyền thống chỉ có một hạt kim cương ở giữa, các nhà thiết kế đã tạo những những chiếc nhẫn vô cùng độc đáo và tinh tế.</LI>
    <LI>Tuy nhiên, giá của đồ trang sức này thì chỉ có dân chơi mới có thể kham được</LI>
</UL>
', N'500000000.0000', N'6', N'49', N'2015-07-23 00:28:05.000', N'25', N'Việt Nam', N'1', N'2', N'0', N'9')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'30', N'Điện thoại Samsung Galaxy A3 2016', N'RAM: 1.5 GB Pin: 2300 mAh', N'<UL>
    <LI>Màn hình: 5.7", Quad HD</LI>
    <LI>Hệ điều hành: Android 6.0</LI>
    <LI>CPU: 8 nhân</LI>
    <LI>RAM: 1.5 GB</LI>
    <LI>ROM: 16 GB</LI>
    <LI>Camera: 16 MP</LI>
    <LI>SIM: 1 Sim</LI>
    <LI>Pin: 2300 mAh, có sạc nhanh</LI>
</UL>

', N'6990000.0000', N'2', N'0', N'2015-11-15 00:28:14.000', N'5', N'Hàn Quốc', N'1', N'4', N'0', N'8')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'43', N'Sách Dạy con làm giàu Tập 1', N'Không có tiền vẫn tạo ra tiền', N'<UL> 
    <LI>Công ty phát hành: NXB Trẻ</LI>
    <LI>Nhà xuất bản: NXB Trẻ</LI>
    <LI>Trọng lượng vận chuyển (gram): 200</LI>
    <LI>Kích thước: 14x20 cm</LI>
    <LI>Tác giả: Robert T Kiyosaki, Sharon L.Lechter</LI>
    <LI>Dịch giả: Thiên Kim</LI>   
    <LI>Số trang: 180 trang</LI>
    <LI>Ngày xuất bản: 01-2010</LI>
</UL>
', N'22000.0000', N'1', N'19', N'2010-10-09 00:28:26.000', N'64', N'Việt Nam', N'3', N'7', N'0', N'2')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'44', N'Truyện Conan Tập 90', N'Thám tử học đường xuất sắc', N'<UL> 
    <LI>Công ty phát hành: Nhà Xuất Bản Kim Đồng</LI>
    <LI>Nhà xuất bản: NXB Kim Đồng</LI>
    <LI>Trọng lượng vận chuyển (gram): 180</LI>
    <LI>Kích thước: 11.3 x 17.6 cm</LI>
    <LI>Tác giả: Aoyama Gosho</LI>
    <LI>Dịch giả: Thiên Kim</LI>   
    <LI>Số trang: 180 trang</LI>
    <LI>Ngày xuất bản: 05-2016</LI>
</UL>', N'19000.0000', N'1', N'9', N'2017-05-18 00:28:40.000', N'83', N'Việt Nam', N'3', N'7', N'0', N'2')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'45', N'Sách Thực Hành Photoshop CC', N'Biên soạn cặn kẽ, chi tiết', N'<UL> 
    <LI>Công ty phát hành: Công Ty TNHH Thương Mại STK</LI>
    <LI>Nhà xuất bản: NXB Khoa học & kỹ thuật</LI>
    <LI>Trọng lượng vận chuyển (gram): 400</LI>
    <LI>Kích thước: 16 x 24 cm</LI>
    <LI>Tác giả: Phạm Quang Huy</LI>
    <LI>Số trang: 382 trang</LI>
    <LI>Ngày xuất bản: 09-2014</LI>
</UL>', N'64000.0000', N'1', N'1', N'2017-05-31 00:28:44.000', N'118', N'Việt Nam', N'3', N'7', N'0', N'10')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'46', N'Sách Đắc Nhân Tâm', N'Cách thức cư xử, ứng xử và giao tiếp', N'<UL> 
    <LI>Công ty phát hành: First News - Trí Việt</LI>
    <LI>Nhà xuất bản: NXB Tổng hợp TP.HCM</LI>
    <LI>Trọng lượng vận chuyển (gram): 320</LI>
    <LI>Kích thước: 14.5 x 20.5 cm</LI>
    <LI>Tác giả: Dale Carnegie</LI>
    <LI>Số trang: 320 trang</LI>
    <LI>Ngày xuất bản: 03-2016</LI>
</UL>', N'61000.0000', N'1', N'8', N'2017-05-12 00:28:49.000', N'28', N'Việt Nam', N'3', N'7', N'0', N'8')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'47', N'Truyện Cổ Grim', N'Tác phẩm văn học kinh điển Đức', N'<UL> 
    <LI>Công ty phát hành: Minh Long</LI>
    <LI>Nhà xuất bản: NXB Văn Học</LI>
    <LI>Trọng lượng vận chuyển (gram): 250</LI>
    <LI>Kích thước: 14.5 x 20.5 cm</LI>   
    <LI>Số trang: 653 trang</LI>
    <LI>Ngày xuất bản: 03-2015</LI>
</UL>', N'110000.0000', N'1', N'0', N'2017-02-28 00:28:53.000', N'58', N'Việt Nam', N'3', N'7', N'0', N'9')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'50', N'Sách Hướng dẫn chạy quảng cáo ', N'Chạy quảng cáo tối ưu', N'<UL> 
    <LI>Công ty phát hành: Phương Thu</LI>
    <LI>Nhà xuất bản: NXB Dân Trí</LI>
    <LI>Trọng lượng vận chuyển (gram): 400</LI>
    <LI>Kích thước: 19 x 27 cm</LI>   
    <LI>Tác giả: Nguyễn Phúc Linh</LI>
    <LI>Số trang: 148 trang</LI>
    <LI>Ngày xuất bản: 12-2016</LI>
</UL>', N'80000.0000', N'1', N'29', N'2017-05-22 00:28:59.000', N'59', N'Việt Nam', N'3', N'7', N'0', N'0')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'52', N'Sách Phong Thủy Cho Văn Phòng', N'Bài trí không gian làm việc ', N'<UL> 
    <LI>Công ty phát hành: First News - Trí Việt</LI>
    <LI>Nhà xuất bản: NXB Trẻ</LI>
    <LI>Trọng lượng vận chuyển (gram): 620</LI>
    <LI>Kích thước: 19 x 26</LI>   
    <LI>Tác giả: Sharon Stasney</LI>
    <LI>Số trang: 128 trang</LI>
    <LI>Ngày xuất bản: 08-2007</LI>
</UL>', N'59000.0000', N'1', N'59', N'2016-02-09 00:29:05.000', N'79', N'Việt Nam', N'3', N'7', N'0', N'35')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'53', N'Canon 700D ', N'Tự động lấy nét', N'<UL> 
    <LI>Độ phân giải camera(MP): 18.0</LI>
    <LI>Mẫu mã: Canon EOS 700D 18MP với Lens KIT 18-55 IS STM (Đen)</LI>
    <LI>Zoom quang: 3.0</LI>
    <LI>Bảo hành: 12 tháng</LI>   
</UL>', N'11900000.0000', N'3', N'9', N'2017-05-10 00:29:20.000', N'57', N'Nhật Bản', N'1', N'8', N'0', N'19')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'54', N'Canon IXUS 95 IS', N'Nhỏ gọn vừa tay', N'<UL> 
    <LI>Độ phân giải camera(MP): 10.0</LI>
    <LI>Màn hình LCD: 2.5 inches</LI>
    <LI>Mẫu mã: CANON IXUS95IS</LI>
    <LI>Zoom quang: 3.0</LI>
    <LI>Bảo hành: 12 tháng</LI>   
    <LI>Xuất xứ: Nhật Bản</LI>  
</UL>', N'6990000.0000', N'3', N'5', N'2017-05-20 00:29:24.000', N'101', N'Nhật Bản', N'2', N'8', N'0', N'29')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'55', N'Sony DSC-W830', N'Chụp thiếu sáng tốt', N'<UL> 
    <LI>Độ phân giải camera(MP): 20.0</LI>
    <LI>Màn hình LCD: 2.5 inches</LI>
    <LI>Mẫu mã: SONY DSC-W830</LI>
    <LI>Zoom quang: 8.0</LI>
    <LI>Bảo hành: 12 tháng</LI>   
    <LI>Xuất xứ: Nhật Bản</LI>  
</UL>', N'2590000.0000', N'3', N'9', N'2017-05-20 00:29:27.000', N'20', N'Nhật Bản', N'2', N'9', N'0', N'2')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'107', N'Chuôt không dây', N'Chuột', N'<ul><li>Chuôt không dây của Tiki<br></li></ul>', N'500.0000', N'7', N'10', N'2017-05-30 00:00:00.000', N'2', N'Viêt Nam', N'1', N'7', N'0', N'0')
GO
GO
INSERT INTO [dbo].[Products] ([ProID], [ProName], [TinyDes], [FullDes], [Price], [CatID], [Quantity], [NgayNhap], [SoLuotXem], [XuatXu], [Loai], [IDNhaSanXuat], [BiXoa], [SoLuongDaBan]) VALUES (N'110', N'chuột Logitech', N'a', N'<ul><li>a</li><li>b</li><li>ádá</li></ul>', N'900000.0000', N'7', N'9', N'2017-05-03 00:00:00.000', N'1', N'Hoa Kỳ', N'1', N'2', N'0', N'0')
GO
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO

-- ----------------------------
-- Table structure for Users
-- ----------------------------
DROP TABLE [dbo].[Users]
GO
CREATE TABLE [dbo].[Users] (
[f_ID] int NOT NULL IDENTITY(1,1) ,
[f_Username] nvarchar(50) NOT NULL ,
[f_Password] nvarchar(255) NOT NULL ,
[f_Name] nvarchar(50) NOT NULL ,
[f_Email] nvarchar(50) NOT NULL ,
[f_DOB] datetime NOT NULL ,
[f_Permission] int NOT NULL DEFAULT ((0)) 
)


GO
DBCC CHECKIDENT(N'[dbo].[Users]', RESEED, 1072)
GO

-- ----------------------------
-- Records of Users
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Users] ON
GO
INSERT INTO [dbo].[Users] ([f_ID], [f_Username], [f_Password], [f_Name], [f_Email], [f_DOB], [f_Permission]) VALUES (N'5', N'nndkhoa', N'E0308DA5BBE8279ADC296567334D429B', N'Khoa N. D. Ngô', N'nndkhoa@a.c', N'2014-02-26 00:00:00.000', N'0')
GO
GO
INSERT INTO [dbo].[Users] ([f_ID], [f_Username], [f_Password], [f_Name], [f_Email], [f_DOB], [f_Permission]) VALUES (N'6', N'tdquang', N'BABA9830D1E5DEAE4954C1364B536D66', N'Trần Duy Quang', N'tdquang@a.c', N'2014-02-18 00:00:00.000', N'0')
GO
GO
INSERT INTO [dbo].[Users] ([f_ID], [f_Username], [f_Password], [f_Name], [f_Email], [f_DOB], [f_Permission]) VALUES (N'8', N'abc', N'E10ADC3949BA59ABBE56E057F20F883E', N'abc', N'abc@a.c', N'2014-03-07 00:00:00.000', N'0')
GO
GO
INSERT INTO [dbo].[Users] ([f_ID], [f_Username], [f_Password], [f_Name], [f_Email], [f_DOB], [f_Permission]) VALUES (N'9', N'admin', N'21232F297A57A5A743894AE4A801FC3', N'Admin', N'admin@g.c', N'2014-03-19 00:00:00.000', N'1')
GO
GO
INSERT INTO [dbo].[Users] ([f_ID], [f_Username], [f_Password], [f_Name], [f_Email], [f_DOB], [f_Permission]) VALUES (N'1021', N'a', N'b59c67bf196a4758191e42f76670ceba', N'a', N'a@g.com', N'2017-06-09 00:00:00.000', N'0')
GO
GO
INSERT INTO [dbo].[Users] ([f_ID], [f_Username], [f_Password], [f_Name], [f_Email], [f_DOB], [f_Permission]) VALUES (N'1071', N'phong', N'B59C67BF196A4758191E42F76670CEBA', N'phongtu', N'a@g.com', N'1999-10-20 00:00:00.000', N'0')
GO
GO
INSERT INTO [dbo].[Users] ([f_ID], [f_Username], [f_Password], [f_Name], [f_Email], [f_DOB], [f_Permission]) VALUES (N'1072', N'aaa', N'B59C67BF196A4758191E42F76670CEBA', N'aaa', N'a@g.c', N'2017-06-08 00:00:00.000', N'0')
GO
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO

-- ----------------------------
-- Indexes structure for table Categories
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Categories
-- ----------------------------
ALTER TABLE [dbo].[Categories] ADD PRIMARY KEY ([CatID])
GO

-- ----------------------------
-- Indexes structure for table NhaSanXuat
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table NhaSanXuat
-- ----------------------------
ALTER TABLE [dbo].[NhaSanXuat] ADD PRIMARY KEY ([IDNhaSanXuat])
GO

-- ----------------------------
-- Indexes structure for table OrderDetails
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table OrderDetails
-- ----------------------------
ALTER TABLE [dbo].[OrderDetails] ADD PRIMARY KEY ([OrderID], [ProID])
GO

-- ----------------------------
-- Indexes structure for table Orders
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Orders
-- ----------------------------
ALTER TABLE [dbo].[Orders] ADD PRIMARY KEY ([OrderID])
GO

-- ----------------------------
-- Indexes structure for table Products
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Products
-- ----------------------------
ALTER TABLE [dbo].[Products] ADD PRIMARY KEY ([ProID])
GO

-- ----------------------------
-- Indexes structure for table Users
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Users
-- ----------------------------
ALTER TABLE [dbo].[Users] ADD PRIMARY KEY ([f_ID])
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[OrderDetails]
-- ----------------------------
ALTER TABLE [dbo].[OrderDetails] ADD FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Orders] ([OrderID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[OrderDetails] ADD FOREIGN KEY ([ProID]) REFERENCES [dbo].[Products] ([ProID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[Orders]
-- ----------------------------
ALTER TABLE [dbo].[Orders] ADD FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([f_ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[Products]
-- ----------------------------
ALTER TABLE [dbo].[Products] ADD FOREIGN KEY ([CatID]) REFERENCES [dbo].[Categories] ([CatID]) ON DELETE NO ACTION ON UPDATE NO ACTION NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Products] ADD FOREIGN KEY ([IDNhaSanXuat]) REFERENCES [dbo].[NhaSanXuat] ([IDNhaSanXuat]) ON DELETE NO ACTION ON UPDATE NO ACTION NOT FOR REPLICATION
GO
