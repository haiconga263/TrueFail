-- MySQL dump 10.13  Distrib 5.5.62, for Win64 (AMD64)
--
-- Host: 192.168.1.201    Database: aritnt
-- ------------------------------------------------------
-- Server version	5.5.62

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `address`
--

DROP TABLE IF EXISTS `address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `address` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `object_type` varchar(1) CHARACTER SET latin1 NOT NULL,
  `object_id` int(11) NOT NULL COMMENT 'The object id is a id of object reference',
  `street` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `country_id` int(11) NOT NULL,
  `province_id` int(11) NOT NULL,
  `district_id` int(11) NOT NULL,
  `ward_id` int(11) NOT NULL,
  `longitude` float DEFAULT NULL,
  `latitude` float DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_address_country_idx` (`country_id`),
  KEY `pk_address_province_idx` (`province_id`),
  KEY `pk_address_district_idx` (`district_id`),
  KEY `pk_address_ward_idx` (`ward_id`),
  CONSTRAINT `pk_address_country` FOREIGN KEY (`country_id`) REFERENCES `country` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_address_district` FOREIGN KEY (`district_id`) REFERENCES `district` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_address_province` FOREIGN KEY (`province_id`) REFERENCES `province` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_address_ward` FOREIGN KEY (`ward_id`) REFERENCES `ward` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `address`
--

LOCK TABLES `address` WRITE;
/*!40000 ALTER TABLE `address` DISABLE KEYS */;
INSERT INTO `address` VALUES (6,'F',8,'địa chỉ farmer1',2,1,1,1,0,0,1,0,'2019-01-03 02:56:17',11,'2019-01-17 02:55:12',11),(11,'R',11,'địa chỉ retailer1',2,1,1,1,0,0,1,0,'2019-01-03 06:40:54',11,'2019-01-11 07:51:36',11),(12,'R',11,'location 1',2,1,1,1,0,0,1,0,'2019-01-03 08:41:56',11,'2019-01-17 04:16:19',19),(13,'C',0,'địa chỉ collection1',2,1,1,1,0,0,1,0,'2019-01-08 05:02:07',11,'2019-01-31 04:52:45',11),(15,'C',7,'địa chỉ collection 2',2,1,1,1,0,0,1,0,'2019-01-08 06:57:01',11,NULL,NULL),(16,'C',8,'địa chỉ fulfillment1',2,1,1,1,0,0,1,0,'2019-01-08 08:03:15',11,NULL,NULL),(17,'C',9,'địa chỉ fulfillment1',2,1,1,1,0,0,1,0,'2019-01-08 08:04:18',11,NULL,NULL),(18,'F',10,'địa chỉ fulfillment1',2,1,1,1,0,0,1,0,'2019-01-08 08:10:38',11,NULL,NULL),(19,'F',1,'địa chỉ fulfillment1',2,1,1,1,0,0,1,0,'2019-01-08 08:15:14',11,'2019-01-17 03:54:59',11),(20,'D',2,'địa chỉ distribution 1',2,1,1,1,0,0,1,0,'2019-01-09 05:00:53',11,'2019-01-17 07:56:13',11),(21,'F',9,'01, Tôn Đức Thắng',2,1,1,1,0,0,1,0,'2019-01-10 09:05:31',11,'2019-01-17 03:54:07',11),(22,'F',10,'01, Tôn Đức Thắng',2,1,1,1,0,0,1,0,'2019-01-10 09:05:40',11,NULL,NULL),(23,'F',11,'01, Tôn Đức Thắng',2,1,1,1,0,0,1,0,'2019-01-10 09:05:41',11,NULL,NULL),(24,'F',12,'01, Tôn Đức Thắng',2,1,1,1,0,0,1,0,'2019-01-10 09:05:42',11,'2019-01-11 07:58:27',11),(25,'F',13,'01, Tôn Đức Thắng',2,1,1,1,0,0,1,0,'2019-01-10 09:05:42',11,'2019-01-10 11:27:57',11),(26,'F',14,'01, Tôn Đức Thắng',2,1,1,1,0,0,1,0,'2019-01-10 09:05:42',11,NULL,NULL),(27,'F',15,'01, Tôn Đức Thắng',2,1,1,1,0,0,1,0,'2019-01-10 09:05:52',11,NULL,NULL),(28,'F',16,'01, Tôn Đức Thắng',2,1,1,1,0,0,1,0,'2019-01-10 09:05:53',11,'2019-01-11 07:58:37',11),(29,'F',17,'01, Tôn Đức Thắng',2,1,1,1,0,0,1,0,'2019-01-10 09:06:08',11,NULL,NULL),(30,'F',18,'01, Tôn Đức Thắng',2,1,1,1,0,0,1,0,'2019-01-10 09:06:27',11,NULL,NULL),(31,'F',19,'01, Tôn Đức Thắng',2,1,1,1,0,0,1,0,'2019-01-10 09:06:28',11,NULL,NULL),(32,'F',20,'01, Tôn Đức Thắng',2,1,1,1,0,0,1,0,'2019-01-10 09:06:28',11,NULL,NULL);
/*!40000 ALTER TABLE `address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `caption`
--

DROP TABLE IF EXISTS `caption`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `caption` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) CHARACTER SET utf8 NOT NULL,
  `type` smallint(6) NOT NULL DEFAULT '1' COMMENT 'type is a column that decribe type of caption.\\n1/ Messgae\\n2/ Lable\\n3/ Column',
  `default_caption` varchar(4000) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=188 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `caption`
--

LOCK TABLES `caption` WRITE;
/*!40000 ALTER TABLE `caption` DISABLE KEYS */;
INSERT INTO `caption` VALUES (1,'NotExistedAccount',1,'Tài khoản không tồn tại'),(2,'WrongPasswordAccount',1,'Mật khẩu không đúng'),(3,'NotActivedAccount',1,'Tài khoản chưa được kích hoạt'),(4,'LockedAccount',1,'Tài khoản đã bị khóa'),(5,'ActivedAccount',1,'Tài khoản đã được kích hoạt rồi'),(6,'ExternalAccount',1,'Tài khoản này là tài khoản external'),(7,'NotEnoughConditionResetPassword',1,'Tài khoản không đủ điều kiện để reset mật khẩu'),(8,'Product.DeletedFailed',1,'Xóa sản phẩm thất bại'),(9,'Image.OutOfLength',1,'Dung lượng hình ảnh quá lớn'),(10,'Image.WrongType',1,'Hình ảnh không đúng kiểu'),(11,'Employee.NotExisted',1,'Nhân viên không tồn tại'),(12,'AddWrongInformation',1,'Thông tin khởi tạo không đúng'),(13,'Employee.ExistedCode',1,'Mã nhân viên đã được sử dụng'),(14,'Product.NotExisted',1,'Sản phẩm không tồn tại'),(15,'Product.ExistedCode',1,'Mã sản phẩm đã được sử dụng'),(16,'Validation.NotEmptyUserName',1,'Tài khoản không được để rỗng'),(17,'Validation.NotEmptyPassword',1,'Mật khẩu không được để rỗng'),(18,'Validation.WrongFormatEmail',1,'Email không đúng định dạng'),(19,'Validation.NotEmptyPinCode',1,'Mã pin không được rỗng'),(20,'Validation.NotEmptyData',1,'Dữ liệu không được để trống'),(21,'Validation.NotEmptyUserId',1,'Mã tài khoản không được để trống'),(22,'Employee.NotExistedManager',1,'Quản lý không tồn tại'),(23,'Employee.DeletedFailed',1,'Xóa nhân viên thật bại'),(24,'Common.AddSuccess',1,'Thêm thành công'),(25,'Alert.ConfirmButton',2,'Ok'),(27,'Country.ExistedCode',1,'Mã quốc gia đã được sử dụng'),(28,'Country.NotExisted',1,'Quốc gia không tồn tại'),(29,'Country.DeletedFailed',1,'Xóa quốc gia thất bại'),(30,'Common.UpdateSuccess',1,'Cập nhật  thành công'),(31,'Region.ExistedCode',1,'Mã khu vực đã được sử dụng'),(32,'Region.NotExisted',1,'Khu vực không tồn tại'),(33,'Province.ExistedCode',1,'Mã Tỉnh/ Thành phố  đã được sử dụng'),(34,'Province.NotExisted',1,'Tỉnh/ Thành phố  không tồn tại'),(36,'District.NotExisted',1,'Quận/ Huyện không tồn tại'),(37,'District.ExistedCode',1,'Mã quận/ huyện đã được sử dụng'),(38,'Ward.NotExisted',1,'Phường/ xã không tồn tại'),(39,'Ward.ExistedCode',1,'Mã phường/ xã đã được sử dụng'),(40,'Retailer.NotExisted',1,'Cửa hàng không tồn tại'),(41,'RetailerLocation.NotExisted',1,'Vị trí cửa hàng không tồn tại'),(42,'RetailerPlanning.AdapFailed',1,'Chỉnh sửa thông tin đáp ứng thất bại'),(43,'Order.NotExisted',1,'Đơn hàng không tồn tại.'),(44,'Order.NotExistedStatus',1,'Trạng thái đơn hàng không tồn tại'),(45,'Order.CantCanceled',1,'Không thể hủy đơn hàng do đã xác nhận'),(46,'Order.WrongStep',1,'Trạng thái không đúng trình tự'),(47,'Collection.NotExisted',1,'Điểm thu mua không tồn tại'),(48,'Collection.ExistedCode',1,'Mã điểm thu mua đã được sử dụng'),(49,'Fulfillment.NotExisted',1,'Điểm xử lý  không tồn tại'),(50,'Fulfillment.ExistedCode',1,'Mã điểm xử lý đã được sử dụng'),(51,'Caption.NotExisted',1,'Từ khóa không tồn tại'),(52,'Vehicle.NotExisted',1,'Xe không tồn tại'),(53,'Order.Retailer.Planning.ExistedCode',1,'Mã kế hoạch đã được sử dụng.'),(54,'Order.Retailer.Planning.NotExisted',1,'Kế hoạch không tồn tại.'),(55,'APIGateway.FailedNetWork',1,'Có lỗi trong quá trình kết nối.'),(56,'Order.Retailer.Order.NotExisted',1,'Đơn mua hàng không tồn tại.'),(57,'Order.Retailer.Order.TimeOut',1,'Đơn mua hàng đã hết thời gian chỉnh sửa.'),(58,'OrderItem.NotExisted',1,'Sản phầm đặt hàng không tồn tại.'),(59,'Action.Refresh',2,'Làm mới'),(60,'Action.Create',2,'Tạo mới'),(61,'Action.Update',2,'Chỉnh sửa'),(62,'Action.Delete',2,'Xóa'),(63,'Action.Save',2,'Lưu lại'),(64,'Action.Return',2,'Trở về'),(65,'Action.Infor',2,'Thông tin chi tiết'),(66,'Action.Confirm',2,'Xác nhận'),(67,'Action.Process',2,'Thực thi'),(68,'Router.NotExisted',1,'Tuyến đường không tồn tại.'),(70,'Common.AddFail',1,'Thêm thất bại.'),(72,'Common.UpdateFail',1,'Cập nhật thất bại.'),(73,'Common.DeleteSuccess',1,'Xóa thành công.'),(74,'Common.DeleteFail',1,'Xóa thất bại.'),(75,'Action.Cancel',2,'Hủy bỏ'),(76,'Router.List',2,'Danh sách tuyến'),(77,'Trip.NotExisted',1,'Chuyến không tồn tại.'),(78,'Trip.NotExistedStatus',1,'Trạng thái chuyến hàng không tồn tại'),(79,'Trip.WrongStep',1,'Trạng thái không đúng trình tự'),(80,'Common.SearchRangeDateWrong',1,'Khoảng thời gian không đúng'),(81,'Common.Code',3,'Mã'),(82,'Delivery.DeliveryMan',3,'NV Giao hàng'),(83,'Delivery.Router',3,'Tuyến'),(84,'Delivery.Driver',3,'Tài xế'),(85,'Delivery.Vehicle',3,'Biển số xe'),(86,'Delivery.DeliveryDate',3,'Ngày giao hàng'),(87,'Common.Status',3,'Trạng thái'),(89,'Common.Address',3,'Địa chỉ'),(90,'Action.Move',3,'Di chuyển'),(91,'Common.FromDate',2,'Từ ngày'),(92,'Common.ToDate',2,'Đến ngày'),(93,'Common.Search',2,'Tìm kiếm'),(94,'Common.Name',3,'Tên'),(95,'Common.TotalAmount',3,'Tổng tiền (VND)'),(96,'MDM.Product.Name',3,'Sản phẩm'),(97,'Common.Price',3,'Giá (VND)'),(98,'Order.OrderedQuantity',3,'Số lượng đặt'),(99,'Order.AdapedQuantity',3,'Số lượng đáp ứng'),(100,'Order.DeliveredQuantity',3,'Số lượng giao'),(101,'MDM.UoM.Name',3,'Đơn vị tính'),(102,'Login.SignInDescription',2,'Đăng nhập để bắt đầu phiên làm việc'),(103,'Login.Username',2,'Tên người dùng'),(104,'Login.Password',2,'Mật khẩu'),(105,'Login.SignIn',2,'Đăng nhập'),(106,'Login.RememberMe',2,'Lưu lại truy cập'),(107,'Login.ForgotPassword',2,'Quên mật khẩu'),(108,'Login.CreateNewAccount',2,'Tạo mới 1 tài khoản'),(109,'Login.UsernameIsRequired',2,'Tên đăng nhập là bắt buộc'),(110,'Login.PasswordIsRequired',2,'Mật khẩu đăng nhập là bắt buộc'),(111,'Alert.TitleSuccess',2,'Thành công'),(112,'Alert.TitleError',2,'Thất bại'),(113,'Alert.TitleWaring',2,'Cảnh báo'),(114,'Alert.TitleInfo',2,'Thông tin'),(115,'Alert.TextSuccess',2,'Tiến trình thành công'),(116,'Alert.TextError',2,'Tiến trình đã xảy ra lỗi'),(117,'Alert.TextWarning',2,' '),(118,'Alert.TextInfo',2,' '),(119,'Alert.ServerBusy',2,'Server đã quá tải, xin hãy quay lại sau'),(120,'Common.Language',3,'Ngôn ngữ'),(121,'Admin.Caption.Name',3,'Tên từ khóa'),(122,'Admin.Caption.Type',3,'Kiểu từ khóa'),(123,'Admin.Caption.DefaultCaption',3,'Mô tả mặc định'),(124,'Common.Description',3,'Mô tả'),(125,'Common.Phone',3,'Số điện thoại'),(126,'Common.Email',3,'Email'),(127,'Common.Active',3,'Hoạt động'),(128,'Validation.Required',1,'Trường dữ liệu bắt buộc nhập'),(129,'Validation.WrongLength',1,'Trường dữ liệu vượt quá độ dài cho phép'),(130,'Admin.Manager',3,'Quản lý'),(131,'Admin.Address.Country',3,'Quốc gia'),(132,'Admin.Address.Province',3,'Tỉnh/Thành phố'),(133,'Admin.Address.District',3,'Quận/Huyện'),(134,'Admin.Address.Ward',3,'Phường/Xã'),(135,'Admin.Address.Street',3,'Địa chỉ'),(136,'Admin.Address.Longitude',3,'Kinh độ'),(137,'Admin.Address.Latitude',3,'Vĩ độ'),(138,'Admin.Contact.FullName',3,'Họ và tên'),(139,'Validation.WrongFormatPhone',1,'Số điện thoại không đúng định dạng'),(140,'Common.Gender',3,'Giới tính'),(141,'Admin.Address',2,'Thông tin địa chỉ'),(142,'Admin.Contact',2,'Thông tin liên lạc'),(143,'Validation.WrongFormatEmail',1,'Email không đúng định dạng'),(144,'Admin.Employee.JobTitle',2,'Chức vụ'),(145,'Common.Account',3,'Tài khoản'),(146,'Common.DayOfBirth',3,'Ngày sinh'),(147,'Common.Tax',3,'Mã doanh nghiệp'),(148,'Common.IsCompany',3,'Là doanh nghiệp'),(149,'Admin.Farmer',2,'Nông dân'),(150,'Order.OrderedDate',3,'Ngày đặt hàng'),(151,'Order.DeliveriedDate',3,'Ngày giao hàng'),(152,'Admin.Product',2,'Sản phẩm'),(153,'Admin.UoM',2,'Đơn vị tính'),(154,'Order.OrderList',3,'Danh sách đặt hàng'),(155,'Order.IsAdap',3,'Có thể đáp ứng'),(156,'Common.CreatedDate',3,'Ngày tại'),(157,'Order.PlaningList',3,'Kế hoạch mua'),(158,'Order.PlanningQuantity',3,'Số lượng dự kiến'),(159,'Admin.Address.Region',3,'Khu vực'),(160,'Admin.Product.DefaultName',3,'Tên mặc định'),(161,'Admin.Product.DefaultBuyingPrice',3,'Giá mua mặc định'),(162,'Admin.Product.DefaultSellingPrice',3,'Giá bán mặc định'),(163,'Common.BuyingPrice',3,'Giá mua'),(164,'Common.SellingPrice',3,'Giá bán'),(165,'Common.EffectivedDate',3,'Ngày hiệu lực'),(166,'Admin.Retailer',2,'Nhà bán lẻ'),(167,'Common.Note',3,'Ghi chú'),(168,'Order.UpdatePlanningMessage',1,'Sửa \"số lượng đáp ứng\" để cập nhật thông tin'),(169,'Admin.Location',2,'Địa điểm'),(170,'Common.ExternalAccount',3,'Tài khoản ngoài'),(171,'User.RoleList',2,'Danh sách quyền'),(172,'User.Role',2,'Quyền'),(173,'Common.Password',3,'Mật khẩu'),(174,'Admin.Vehicle',2,'Xe'),(175,'Admin.Vehicle.Type',2,'Loại xe'),(176,'Admin.Vehicle.Weight',2,'Trọng lượng'),(177,'Admin.Vehicle.Volumn',2,'Thể tích'),(178,'Order.DeliveriedLocation',2,'Nơi nhận hàng'),(179,'Order.BillLocation',2,'Nơi nhận hóa đơn'),(180,'Common.Information',2,'Thông tin chung'),(181,'Order.Detail',2,'Chi tiết đơn hàng'),(182,'Retailer.OrderTracing.OrderedMessage',2,'Bạn đã đặt hàng vào'),(183,'Retailer.OrderTracing.ConfirmedMessage',2,'Đơn hàng được xác nhận vào'),(184,'Retailer.OrderTracing.DeliveriedMessage',2,'Đơn hàng được vận chuyển vào'),(185,'Retailer.OrderTracing.CompletedMessage',2,'Đơn hàng hoàn tất vào'),(186,'Retailer.OrderTracing.CanceledMessage',2,'Đơn hàng bị hủy vào'),(187,'Common.Warning',2,'Cảnh báo');
/*!40000 ALTER TABLE `caption` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `caption_language`
--

DROP TABLE IF EXISTS `caption_language`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `caption_language` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `language_id` int(11) NOT NULL,
  `caption_id` bigint(20) NOT NULL,
  `caption` varchar(4000) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_caption_language_caption_idx` (`caption_id`),
  KEY `pk_caption_language_language_idx` (`language_id`),
  CONSTRAINT `pk_caption_language_caption` FOREIGN KEY (`caption_id`) REFERENCES `caption` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_caption_language_language` FOREIGN KEY (`language_id`) REFERENCES `language` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `caption_language`
--

LOCK TABLES `caption_language` WRITE;
/*!40000 ALTER TABLE `caption_language` DISABLE KEYS */;
INSERT INTO `caption_language` VALUES (11,1,1,'Tài khoản không tồn tại'),(12,2,1,'Account isn\'t existed'),(13,1,85,'Plate number'),(14,2,85,'Biển số xe'),(15,1,2,'Mật khẩu không đúng'),(16,2,2,'Password was wrong'),(17,1,3,'Tài khoản chưa được kích hoạt'),(18,2,3,'This account isn\'t actived'),(19,1,4,'Tài khoản đã bị khóa'),(20,2,4,'This account is locked'),(21,1,5,'Tài khoản đã được kích hoạt rồi'),(22,2,5,'This account is already actived'),(23,1,6,'Tài khoản này là tài khoản external'),(24,2,6,'This account is a external user.'),(25,1,7,'Tài khoản không đủ điều kiện để reset mật khẩu'),(26,2,7,'This account isn\'t enough condition to reset pasword'),(27,1,8,'Xóa sản phẩm thất bại'),(28,2,8,'Deleted product failed'),(35,1,81,'Mã'),(36,2,81,'Code'),(37,1,129,''),(38,2,129,'');
/*!40000 ALTER TABLE `caption_language` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `collection`
--

DROP TABLE IF EXISTS `collection`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `collection` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET latin1 NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `image_url` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `contact_id` int(11) DEFAULT NULL,
  `address_id` int(11) DEFAULT NULL,
  `manager_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_collection_address_idx` (`address_id`),
  KEY `pk_collection_contact_idx` (`contact_id`),
  KEY `pk_collection_employee_idx` (`manager_id`),
  CONSTRAINT `pk_collection_address` FOREIGN KEY (`address_id`) REFERENCES `address` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_collection_contact` FOREIGN KEY (`contact_id`) REFERENCES `contact` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_collection_employee` FOREIGN KEY (`manager_id`) REFERENCES `employee` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `collection`
--

LOCK TABLES `collection` WRITE;
/*!40000 ALTER TABLE `collection` DISABLE KEYS */;
INSERT INTO `collection` VALUES (5,'C00000001','Collection1','/Images/Collection/201901/9a3dfd99ff58445dbd7a4a125f7242dd.jpeg',14,13,8,1,0,'2019-01-08 06:37:36',11,'2019-01-31 04:52:45',11),(7,'C000000002','Collection2','/Images/Collection/201901/d7be5cf5dcdf4b098e966602cffafe0d.jpeg',16,15,2,1,0,'2019-01-08 06:56:59',11,NULL,NULL),(8,'F000000001','Fulfillment1','/Images/Fulfillment/201901/b4d09395b59442769460912809bf5da2.jpeg',17,16,2,1,1,'2019-01-08 08:12:16',11,'2019-01-08 08:12:15',11),(9,'F00000001','Fulfillment1','/Images/Fulfillment/201901/dd320078a5ef453dbef347b78332ffc6.jpeg',18,17,2,1,1,'2019-01-08 08:12:18',11,'2019-01-08 08:12:17',11),(10,'F00000001','Fulfillment1','/Images/Fulfillment/201901/de02e277b82a4524bd8072f4f01aea45.jpeg',19,18,2,1,1,'2019-01-08 08:12:20',11,'2019-01-08 08:12:19',11);
/*!40000 ALTER TABLE `collection` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `collection_employee`
--

DROP TABLE IF EXISTS `collection_employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `collection_employee` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `collection_id` int(11) NOT NULL,
  `employee_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_collection_employee_idx` (`collection_id`),
  KEY `fk_collection_employee_employee_idx` (`employee_id`),
  CONSTRAINT `fk_collection_employee_collection` FOREIGN KEY (`collection_id`) REFERENCES `collection` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_collection_employee_employee` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `collection_employee`
--

LOCK TABLES `collection_employee` WRITE;
/*!40000 ALTER TABLE `collection_employee` DISABLE KEYS */;
/*!40000 ALTER TABLE `collection_employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contact`
--

DROP TABLE IF EXISTS `contact`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contact` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `object_type` varchar(1) CHARACTER SET latin1 NOT NULL,
  `object_id` int(11) NOT NULL COMMENT 'The object id is a id of object reference',
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `phone` varchar(15) CHARACTER SET latin1 DEFAULT NULL,
  `email` varchar(100) CHARACTER SET latin1 DEFAULT NULL,
  `gender` char(1) CHARACTER SET latin1 DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contact`
--

LOCK TABLES `contact` WRITE;
/*!40000 ALTER TABLE `contact` DISABLE KEYS */;
INSERT INTO `contact` VALUES (6,'F',8,'Nguyễn Văn Farmer1','0934162993','hoangnguyen@aritnt.com.vn','M',1,0,'2019-01-03 02:56:17',11,'2019-01-17 02:55:13',11),(12,'R',11,'Nguyễn Văn Retailer1','0934162993','hoangnguyen@aritnt.com.vn','M',1,0,'2019-01-03 06:40:54',11,'2019-01-11 07:51:36',11),(13,'R',11,'Location 1 - retailer1','0934162993','hoangnguyen@aritnt.com.vn','M',1,0,'2019-01-03 08:42:01',11,'2019-01-17 04:16:19',19),(14,'C',0,'Nguyễn Huy Hoàng','0934162993','hoangnguyen@aritnt.com.vn','M',1,0,'2019-01-08 05:02:09',11,'2019-01-31 04:52:45',11),(16,'C',7,'Nguyễn Huy Hoàng','0934162993','hoangnguyen@aritnt.com.vn','M',1,0,'2019-01-08 06:57:01',11,NULL,NULL),(17,'C',8,'Nguyễn Huy Hoàng','0934162993','hoangnguyen@aritnt.com.vn','M',1,0,'2019-01-08 08:03:15',11,NULL,NULL),(18,'C',9,'Nguyễn Huy Hoàng','0934162993','hoangnguyen@aritnt.com.vn','',1,0,'2019-01-08 08:04:18',11,NULL,NULL),(19,'F',10,'Nguyễn Huy Hoàng','0934162993','hoangnguyen@aritnt.com.vn','M',1,0,'2019-01-08 08:10:47',11,NULL,NULL),(20,'F',1,'Nguyễn Huy Hoàng','0934162993','hoangnguyen@aritnt.com.vn','M',1,0,'2019-01-08 08:15:14',11,'2019-01-17 03:54:59',11),(21,'D',2,'Nguyễn Huy Hoàng','0934162993','hoangnguyen@aritnt.com.vn','M',1,0,'2019-01-09 05:00:53',11,'2019-01-17 07:56:13',11),(22,'F',9,'Nguyễn Huy Hoàng','0934162993','hoang@aritnt.com.vn','M',1,0,'2019-01-10 09:05:31',11,'2019-01-17 03:54:07',11),(23,'F',10,'Nguyễn Huy Hoàng','0934162993','hoang@aritnt.com.vn','M',1,0,'2019-01-10 09:05:40',11,NULL,NULL),(24,'F',11,'Nguyễn Huy Hoàng','0934162993','hoang@aritnt.com.vn','M',1,0,'2019-01-10 09:05:41',11,NULL,NULL),(25,'F',12,'Nguyễn Huy Hoàng','0934162993','hoang@aritnt.com.vn','M',1,0,'2019-01-10 09:05:42',11,'2019-01-11 07:58:27',11),(26,'F',13,'Nguyễn Huy Hoàng','0934162993','hoang@aritnt.com.vn','M',1,0,'2019-01-10 09:05:42',11,'2019-01-10 11:27:57',11),(27,'F',14,'Nguyễn Huy Hoàng','0934162993','hoang@aritnt.com.vn','M',1,0,'2019-01-10 09:05:42',11,NULL,NULL),(28,'F',15,'Nguyễn Huy Hoàng','0934162993','hoang@aritnt.com.vn','M',1,0,'2019-01-10 09:05:52',11,NULL,NULL),(29,'F',16,'Nguyễn Huy Hoàng','0934162993','hoang@aritnt.com.vn','M',1,0,'2019-01-10 09:05:53',11,'2019-01-11 07:58:37',11),(30,'F',17,'Nguyễn Huy Hoàng','0934162993','hoang@aritnt.com.vn','M',1,0,'2019-01-10 09:06:08',11,NULL,NULL),(31,'F',18,'Nguyễn Huy Hoàng','0934162993','hoang@aritnt.com.vn','M',1,0,'2019-01-10 09:06:27',11,NULL,NULL),(32,'F',19,'Nguyễn Huy Hoàng','0934162993','hoang@aritnt.com.vn','M',1,0,'2019-01-10 09:06:28',11,NULL,NULL),(33,'F',20,'Nguyễn Huy Hoàng','0934162993','hoang@aritnt.com.vn','M',1,0,'2019-01-10 09:06:28',11,NULL,NULL);
/*!40000 ALTER TABLE `contact` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `country`
--

DROP TABLE IF EXISTS `country`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `country` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET latin1 NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `phone_code` varchar(10) CHARACTER SET latin1 NOT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` int(11) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `country`
--

LOCK TABLES `country` WRITE;
/*!40000 ALTER TABLE `country` DISABLE KEYS */;
INSERT INTO `country` VALUES (2,'VN','Việt Nam','+84',1,0,'2018-12-31 09:11:39',11,'2018-12-31 09:55:17',11),(3,'CN','Trung Quốc','+86',1,1,'2018-12-31 09:55:36',11,'2018-12-31 10:33:42',11),(4,'America','Hoa Kỳ','4334',1,0,'2019-01-02 03:17:59',11,NULL,NULL);
/*!40000 ALTER TABLE `country` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `distribution`
--

DROP TABLE IF EXISTS `distribution`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `distribution` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET latin1 NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `image_url` varchar(100) CHARACTER SET latin1 DEFAULT NULL,
  `contact_id` int(11) DEFAULT NULL,
  `address_id` int(11) DEFAULT NULL,
  `manager_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_distribution_contact_idx` (`contact_id`),
  KEY `pk_distribution_address_idx` (`address_id`),
  KEY `pk_distribution_employee_idx` (`manager_id`),
  CONSTRAINT `pk_distribution_address` FOREIGN KEY (`address_id`) REFERENCES `address` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_distribution_contact` FOREIGN KEY (`contact_id`) REFERENCES `contact` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_distribution_employee` FOREIGN KEY (`manager_id`) REFERENCES `employee` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `distribution`
--

LOCK TABLES `distribution` WRITE;
/*!40000 ALTER TABLE `distribution` DISABLE KEYS */;
INSERT INTO `distribution` VALUES (2,'D000000001','Distribution 1','/Images/Distribution/201901/f9e5cf4422214bf39d282f0eb0a8807a.jpeg',21,20,4,1,0,'2019-01-09 05:00:53',11,'2019-01-17 07:56:13',11);
/*!40000 ALTER TABLE `distribution` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `district`
--

DROP TABLE IF EXISTS `district`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `district` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET latin1 NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `country_id` int(11) NOT NULL,
  `province_id` int(11) NOT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_district_country_idx` (`country_id`),
  KEY `pk_district_province_idx` (`province_id`),
  CONSTRAINT `pk_district_country` FOREIGN KEY (`country_id`) REFERENCES `country` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_district_province` FOREIGN KEY (`province_id`) REFERENCES `province` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `district`
--

LOCK TABLES `district` WRITE;
/*!40000 ALTER TABLE `district` DISABLE KEYS */;
INSERT INTO `district` VALUES (1,'Q1','Quận 1',2,1,1,0,'2019-01-02 04:14:53',11,'2019-01-02 04:26:20',11),(2,'Q2','Quận 2',2,1,1,0,'2019-01-02 04:26:10',11,'2019-01-02 04:26:33',11);
/*!40000 ALTER TABLE `district` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `employee` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET utf8 NOT NULL,
  `full_name` varchar(50) CHARACTER SET utf8 NOT NULL,
  `user_account_id` int(11) DEFAULT NULL,
  `report_to` int(11) DEFAULT NULL,
  `report_to_code` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `email` varchar(100) NOT NULL,
  `phone` varchar(15) NOT NULL,
  `birthday` date DEFAULT NULL,
  `gender` char(1) NOT NULL,
  `job_title` varchar(50) CHARACTER SET utf8 NOT NULL,
  `image_url` varchar(100) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_employee_employee_report_to_idx` (`report_to`),
  CONSTRAINT `pk_employee_employee_report_to` FOREIGN KEY (`report_to`) REFERENCES `employee` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee` VALUES (2,'NV00000001','Nguyễn Huy Hoàng',10,NULL,NULL,'hoangnguyen@aritnt.com.vn','0934162993','1993-08-02','F','Administrator','/Images/Employee/201901/0320f6acbf0d4028b1bb5f04165541d0.jpeg',1,0,'2018-12-28 06:37:55',11,'2019-01-17 03:54:41',11),(3,'NV00000002','Lã Thị Hậu',16,2,'','haula@aritnt.com.vn','0388061306','1993-07-28','M','QC Staff','/201812/2035241d6712471abf1c9d2cb82a7317.png',1,0,'2018-12-28 07:14:55',11,'2019-01-03 02:46:54',11),(4,'NV00000003','Distributor',26,NULL,NULL,'distributor@aritnt.com.vn','0934162993','2019-01-06','M','Giám sát nhà phân phối','/Images/Employee/201901/18c60ae36aa64df49ea8393ee4eefab9.jpeg',1,0,'2019-01-17 07:55:35',11,'2019-01-17 07:56:01',11),(5,'NV00000004','Delivery Man',27,4,'','deliveryman@aritnt.com.vn','0934162993','1993-08-01','M','Delivery Man','',1,0,'2019-01-21 10:00:53',11,NULL,NULL),(6,'NV00000005','Driver',28,4,'','driver@aritnt.com.vn','0934162993','2019-01-06','F','Driver','',1,0,'2019-01-21 10:04:24',11,NULL,NULL),(7,'NV000001','Võ Thanh Thiên',0,2,'','thienvo@aritnt.com.vn','0334883344','2020-01-23','M','Quản lý quận 1','',1,0,'2019-01-22 10:54:17',11,'2019-01-22 11:01:30',11),(8,'NV00000006','Collector',29,2,'','Collector@aritnt.com.vn','01234156556','2019-01-14','M','Collector','',1,0,'2019-01-29 10:27:03',11,'2019-01-29 10:29:08',11);
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `farmer`
--

DROP TABLE IF EXISTS `farmer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `farmer` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_account_id` int(11) DEFAULT NULL,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `is_company` tinyint(4) NOT NULL,
  `tax_code` varchar(15) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `image_url` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `contact_id` int(11) DEFAULT NULL,
  `address_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL DEFAULT '0',
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `farmer`
--

LOCK TABLES `farmer` WRITE;
/*!40000 ALTER TABLE `farmer` DISABLE KEYS */;
INSERT INTO `farmer` VALUES (8,18,'Nguyễn Văn Farmer1',1,'MACODECONGTY1','/Images/Farmer/201901/6ed6099b3e0a436cb114f2041063819e.jpeg',6,6,1,0,'2019-01-03 02:56:17',11,'2019-01-17 02:55:13',11);
/*!40000 ALTER TABLE `farmer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `farmer_buying_calendar`
--

DROP TABLE IF EXISTS `farmer_buying_calendar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `farmer_buying_calendar` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(50) CHARACTER SET latin1 NOT NULL,
  `farmer_id` int(11) NOT NULL,
  `buying_date` date NOT NULL,
  `is_ordered` tinyint(4) NOT NULL DEFAULT '0',
  `is_expired` tinyint(4) NOT NULL DEFAULT '0',
  `is_adaped` tinyint(4) NOT NULL,
  `adap_note` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_farmer_buying_calendar_farmer_idx` (`farmer_id`),
  CONSTRAINT `pk_farmer_buying_calendar_farmer` FOREIGN KEY (`farmer_id`) REFERENCES `farmer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `farmer_buying_calendar`
--

LOCK TABLES `farmer_buying_calendar` WRITE;
/*!40000 ALTER TABLE `farmer_buying_calendar` DISABLE KEYS */;
INSERT INTO `farmer_buying_calendar` VALUES (1,'P201900001','Planning Test',8,'2019-01-08',0,0,1,NULL,0,'2019-01-06 17:00:00',11,NULL,NULL),(2,'P201900002','Planning Test History',8,'2019-01-07',1,0,1,NULL,0,'2019-01-06 17:00:00',11,NULL,NULL);
/*!40000 ALTER TABLE `farmer_buying_calendar` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `farmer_buying_calendar_item`
--

DROP TABLE IF EXISTS `farmer_buying_calendar_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `farmer_buying_calendar_item` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `farmer_buying_calendar_id` bigint(20) NOT NULL,
  `product_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `adap_quantity` int(11) NOT NULL,
  `uom_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_farmer_buying_calendar_item_farmer_buying_calendar_idx` (`farmer_buying_calendar_id`),
  KEY `pk_farmer_buying_calendar_item_product_idx` (`product_id`),
  KEY `pk_farmer_buying_calendar_item_uom_idx` (`uom_id`),
  CONSTRAINT `pk_farmer_buying_calendar_item_farmer_buying_calendar` FOREIGN KEY (`farmer_buying_calendar_id`) REFERENCES `farmer_buying_calendar` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_farmer_buying_calendar_item_product` FOREIGN KEY (`product_id`) REFERENCES `product` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_farmer_buying_calendar_item_uom` FOREIGN KEY (`uom_id`) REFERENCES `uom` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `farmer_buying_calendar_item`
--

LOCK TABLES `farmer_buying_calendar_item` WRITE;
/*!40000 ALTER TABLE `farmer_buying_calendar_item` DISABLE KEYS */;
INSERT INTO `farmer_buying_calendar_item` VALUES (1,1,1,100,0,1),(2,1,2,1000,0,2),(3,2,1,100,100,1),(4,2,1,999,999,2);
/*!40000 ALTER TABLE `farmer_buying_calendar_item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `farmer_order`
--

DROP TABLE IF EXISTS `farmer_order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `farmer_order` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET latin1 NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `farmer_id` int(11) NOT NULL,
  `collection_id` int(11) DEFAULT NULL,
  `farmer_buying_calendar_id` bigint(20) DEFAULT NULL,
  `status_id` int(11) NOT NULL,
  `buying_date` date NOT NULL,
  `ship_to` int(11) NOT NULL,
  `total_amount` decimal(10,0) NOT NULL,
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_farmer_order_farmer_idx` (`farmer_id`),
  KEY `pk_farmer_order_farmer_buying_calendar_idx` (`farmer_buying_calendar_id`),
  KEY `pk_farmer_order_farmer_order_status_idx` (`status_id`),
  KEY `pk_farmer_order_collection_idx` (`ship_to`),
  KEY `fk_farmer_order_collection_idx` (`collection_id`),
  CONSTRAINT `fk_farmer_order_collection` FOREIGN KEY (`collection_id`) REFERENCES `collection` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_farmer_order_collection` FOREIGN KEY (`ship_to`) REFERENCES `collection` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_farmer_order_farmer` FOREIGN KEY (`farmer_id`) REFERENCES `farmer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_farmer_order_farmer_buying_calendar` FOREIGN KEY (`farmer_buying_calendar_id`) REFERENCES `farmer_buying_calendar` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_farmer_order_farmer_order_status` FOREIGN KEY (`status_id`) REFERENCES `farmer_order_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `farmer_order`
--

LOCK TABLES `farmer_order` WRITE;
/*!40000 ALTER TABLE `farmer_order` DISABLE KEYS */;
INSERT INTO `farmer_order` VALUES (1,'O201900001','Đặt hàng Rau cải',8,5,NULL,2,'2019-02-01',5,20000000,0,'2019-01-30 17:00:00',11,NULL,NULL);
/*!40000 ALTER TABLE `farmer_order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `farmer_order_item`
--

DROP TABLE IF EXISTS `farmer_order_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `farmer_order_item` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `farmer_order_id` bigint(20) NOT NULL,
  `product_id` int(11) NOT NULL,
  `status_id` int(11) NOT NULL,
  `price` decimal(10,0) NOT NULL,
  `ordered_quantity` int(11) NOT NULL,
  `deliveried_quantity` int(11) NOT NULL,
  `uom_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_farmer_order_item_farmer_order_idx` (`farmer_order_id`),
  KEY `pk_farmer_order_item_product_idx` (`product_id`),
  KEY `pk_farmer_order_item_farmer_order_status_idx` (`status_id`),
  KEY `pk_farmer_order_item_uom_idx` (`uom_id`),
  CONSTRAINT `pk_farmer_order_item_farmer_order` FOREIGN KEY (`farmer_order_id`) REFERENCES `farmer_order` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_farmer_order_item_farmer_order_status` FOREIGN KEY (`status_id`) REFERENCES `farmer_order_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_farmer_order_item_product` FOREIGN KEY (`product_id`) REFERENCES `product` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_farmer_order_item_uom` FOREIGN KEY (`uom_id`) REFERENCES `uom` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `farmer_order_item`
--

LOCK TABLES `farmer_order_item` WRITE;
/*!40000 ALTER TABLE `farmer_order_item` DISABLE KEYS */;
INSERT INTO `farmer_order_item` VALUES (1,1,1,2,20000,1000,0,1);
/*!40000 ALTER TABLE `farmer_order_item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `farmer_order_status`
--

DROP TABLE IF EXISTS `farmer_order_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `farmer_order_status` (
  `id` int(11) NOT NULL,
  `caption_name` varchar(50) NOT NULL,
  `caption_decription` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `farmer_order_status`
--

LOCK TABLES `farmer_order_status` WRITE;
/*!40000 ALTER TABLE `farmer_order_status` DISABLE KEYS */;
INSERT INTO `farmer_order_status` VALUES (-1,'FarmerStatus.Name.Canceled','FarmerStatus.Description.Canceled'),(1,'FarmerStatus.Name.Ordered','FarmerStatus.Description.Ordered'),(2,'FarmerStatus.Name.ComfirmedOrder','FarmerStatus.Description.ComfirmedOrder'),(3,'FarmerStatus.Name.Delivering','FarmerStatus.Description.Delivering'),(4,'FarmerStatus.Name.Completed','FarmerStatus.Description.Completed');
/*!40000 ALTER TABLE `farmer_order_status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `farmer_retailer_order_items`
--

DROP TABLE IF EXISTS `farmer_retailer_order_items`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `farmer_retailer_order_items` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `retailer_id` int(11) NOT NULL,
  `retailer_order_id` bigint(20) NOT NULL,
  `retailer_order_item_id` bigint(20) NOT NULL,
  `farmer_id` int(11) NOT NULL,
  `farmer_order_id` bigint(20) NOT NULL,
  `farmer_order_item_id` bigint(20) NOT NULL,
  `product_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `uom_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_farmer_retailer_order_items_retailer_idx` (`retailer_id`),
  KEY `pk_farmer_retailer_order_items_farmer_idx` (`farmer_id`),
  KEY `pk_farmer_retailer_order_items_product_idx` (`product_id`),
  KEY `pk_farmer_retailer_order_items_uom_idx` (`uom_id`),
  CONSTRAINT `pk_farmer_retailer_order_items_farmer` FOREIGN KEY (`farmer_id`) REFERENCES `farmer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_farmer_retailer_order_items_product` FOREIGN KEY (`product_id`) REFERENCES `product` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_farmer_retailer_order_items_retailer` FOREIGN KEY (`retailer_id`) REFERENCES `retailer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_farmer_retailer_order_items_uom` FOREIGN KEY (`uom_id`) REFERENCES `uom` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `farmer_retailer_order_items`
--

LOCK TABLES `farmer_retailer_order_items` WRITE;
/*!40000 ALTER TABLE `farmer_retailer_order_items` DISABLE KEYS */;
/*!40000 ALTER TABLE `farmer_retailer_order_items` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fulfillment`
--

DROP TABLE IF EXISTS `fulfillment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fulfillment` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) NOT NULL,
  `name` varchar(50) NOT NULL,
  `image_url` varchar(100) DEFAULT NULL,
  `contact_id` int(11) DEFAULT NULL,
  `address_id` int(11) DEFAULT NULL,
  `manager_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_fulfillment_contact_idx` (`contact_id`),
  KEY `pk_fulfillment_address_idx` (`address_id`),
  KEY `pk_fulfillment_employee_idx` (`manager_id`),
  CONSTRAINT `pk_fulfillment_address` FOREIGN KEY (`address_id`) REFERENCES `address` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_fulfillment_contact` FOREIGN KEY (`contact_id`) REFERENCES `contact` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_fulfillment_employee` FOREIGN KEY (`manager_id`) REFERENCES `employee` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fulfillment`
--

LOCK TABLES `fulfillment` WRITE;
/*!40000 ALTER TABLE `fulfillment` DISABLE KEYS */;
INSERT INTO `fulfillment` VALUES (1,'F00000001','Fulfillment1','/Images/Fulfillment/201901/227aa02257bc46b7922ed990b1e28355.jpeg',20,19,2,1,0,'2019-01-08 08:17:50',11,'2019-01-17 03:54:59',11);
/*!40000 ALTER TABLE `fulfillment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `language`
--

DROP TABLE IF EXISTS `language`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `language` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) CHARACTER SET utf8 NOT NULL,
  `code` varchar(10) CHARACTER SET utf8 NOT NULL,
  `description` varchar(256) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `language`
--

LOCK TABLES `language` WRITE;
/*!40000 ALTER TABLE `language` DISABLE KEYS */;
INSERT INTO `language` VALUES (1,'VietNam','vi','Việt Nam'),(2,'English','en','English');
/*!40000 ALTER TABLE `language` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET latin1 NOT NULL,
  `image_url` varchar(100) CHARACTER SET latin1 DEFAULT NULL,
  `default_name` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `default_description` varchar(255) DEFAULT NULL,
  `default_uom_id` int(11) NOT NULL DEFAULT '1',
  `default_buying_price` decimal(10,0) NOT NULL,
  `default_selling_price` decimal(10,0) NOT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,'SP00000001','/Images/Employee/201901/75f62cef9d824645b0b6393391355f67.jpeg','Sản phẩm 1',NULL,1,20,30,1,0,'2018-12-27 06:52:46',11,'2019-01-17 03:54:32',11),(2,'SP00000002','/Images/Product/201812/82522ded90b64a35840208eab5584205.png','Sản phẩm 2',NULL,1,2,5,1,0,'2018-12-27 06:55:06',11,'2018-12-27 10:10:40',11),(3,'SP00000003','','Sản phẩm 3',NULL,1,3,4,1,1,'2018-12-27 06:57:58',11,'2018-12-27 10:26:29',11),(4,'SP00000004','/201812/d1b18f2041d3417489597b373b71e00c.jpeg','Sản phẩm 4',NULL,1,4,10,1,0,'2018-12-27 06:59:05',11,'2018-12-31 10:22:55',11),(5,'SP00000005','','Sản phẩm 5','Đây là sản phẩm 5',1,0,0,1,0,'2018-12-27 07:09:11',11,'2018-12-27 07:38:15',11),(6,'SP00000006','/201812/982bf13bc6c645b08f62b8db066bebbf.jpeg','Sản phẩm 6','Đây là sản phẩm 6',1,3,2,1,0,'2018-12-27 07:12:58',11,'2018-12-27 09:52:58',11),(7,'SP0000007','','S?n ph?m 7',NULL,1,3,4,1,0,'2018-12-27 07:16:17',11,'2019-01-28 09:45:53',11),(8,'SM00000008','','Sản phẩm 8',NULL,1,4,5,1,0,'2018-12-27 07:19:26',11,'2019-01-28 09:46:06',11),(9,'SM00000009','','Sản phẩm 9',NULL,1,2,8,1,0,'2018-12-27 07:21:20',11,'2019-01-28 09:46:19',11),(10,'SP00000010','','Sản phẩm 10','Đây là sản phẩm 10',1,4,2,1,0,'2018-12-27 07:25:40',11,NULL,NULL),(11,'SP00000011','/Images/Product/201812/5589e11380cd495e868f2a05b96131cb.png','Sản phẩm 11','Đây là sản phẩm 11',1,10,20,1,0,'2018-12-27 10:02:54',11,NULL,NULL),(12,'PM000001','/Images/Product/201901/1f201549a5a64b92993bd1b2642be174.png','Cà chua','Màu đỏ, xanh, vàng',1,7,10,1,0,'2019-01-10 07:20:59',11,'2019-01-11 07:20:48',11),(13,'MP00002','/Images/Product/201901/8fceb4144ba0413db947d62493c15187.png','Dưa leo','Dưa leo nhật bản',1,30000,50000,1,0,'2019-01-15 02:33:02',11,NULL,NULL);
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_language`
--

DROP TABLE IF EXISTS `product_language`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_language` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `product_id` int(11) NOT NULL,
  `language_id` int(11) NOT NULL,
  `name` varchar(50) CHARACTER SET utf8 NOT NULL,
  `description` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_product_language_language_idx` (`language_id`),
  KEY `pk_product_language_product_idx` (`product_id`),
  CONSTRAINT `pk_product_language_language` FOREIGN KEY (`language_id`) REFERENCES `language` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_product_language_product` FOREIGN KEY (`product_id`) REFERENCES `product` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_language`
--

LOCK TABLES `product_language` WRITE;
/*!40000 ALTER TABLE `product_language` DISABLE KEYS */;
INSERT INTO `product_language` VALUES (1,6,1,'1312312','312312312'),(2,6,2,'sfsdfsdfsdf','sdfsdfsdfsd'),(3,11,1,'Sản phẩm 11','Đây là sản phẩm 11'),(4,11,2,'Product No.11','This is Product No.11'),(5,4,1,'Sản phẩm 4','Sản phẩm 4'),(6,4,2,'Product 4','Product 4'),(7,12,1,'Cà chua','Có màu đỏ, xanh,vàng'),(8,12,2,'Tomato','Have red, green, yellow color'),(9,1,1,'Sản phẩm 1','Sản phẩm 1'),(10,1,2,'Product 1','Product 1'),(11,13,1,'',''),(12,13,2,'',''),(13,9,1,'',''),(14,9,2,'',''),(15,7,1,'',''),(16,7,2,'',''),(17,8,1,'',''),(18,8,2,'','');
/*!40000 ALTER TABLE `product_language` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_price`
--

DROP TABLE IF EXISTS `product_price`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_price` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `product_id` int(11) NOT NULL,
  `uom_id` int(11) DEFAULT NULL,
  `buying_price` decimal(10,0) NOT NULL,
  `selling_price` decimal(10,0) NOT NULL,
  `effectived_date` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_product_price_product_idx` (`product_id`),
  KEY `pk_product_price_uom_idx` (`uom_id`),
  CONSTRAINT `pk_product_price_product` FOREIGN KEY (`product_id`) REFERENCES `product` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_product_price_uom` FOREIGN KEY (`uom_id`) REFERENCES `uom` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_price`
--

LOCK TABLES `product_price` WRITE;
/*!40000 ALTER TABLE `product_price` DISABLE KEYS */;
INSERT INTO `product_price` VALUES (5,4,1,2030394,234324,'2019-12-23 17:00:00'),(6,4,2,243232,234234,'2018-12-26 00:00:00'),(7,4,1,575678568,76876867,'2018-12-26 00:00:00'),(8,12,4,100000,120000,'2019-01-10 17:00:00'),(9,1,1,20000,30000,'2019-01-09 17:00:00');
/*!40000 ALTER TABLE `product_price` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `province`
--

DROP TABLE IF EXISTS `province`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `province` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET latin1 NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `phone_code` varchar(10) CHARACTER SET latin1 DEFAULT NULL,
  `country_id` int(11) NOT NULL,
  `region_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_province_country_idx` (`country_id`),
  KEY `pk_province_region_idx` (`region_id`),
  CONSTRAINT `pk_province_country` FOREIGN KEY (`country_id`) REFERENCES `country` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_province_region` FOREIGN KEY (`region_id`) REFERENCES `region` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `province`
--

LOCK TABLES `province` WRITE;
/*!40000 ALTER TABLE `province` DISABLE KEYS */;
INSERT INTO `province` VALUES (1,'HCM','Thành Phố Hồ Chí Minh','083',2,1,1,0,'2019-01-02 02:29:25',11,'2019-01-02 05:05:16',11);
/*!40000 ALTER TABLE `province` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `region`
--

DROP TABLE IF EXISTS `region`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `region` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET latin1 NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `country_id` int(11) NOT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_region_country_idx` (`country_id`),
  CONSTRAINT `pk_region_country` FOREIGN KEY (`country_id`) REFERENCES `country` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `region`
--

LOCK TABLES `region` WRITE;
/*!40000 ALTER TABLE `region` DISABLE KEYS */;
INSERT INTO `region` VALUES (1,'DBSCL','Đồng bằng sông Cửu Long',2,1,0,'2018-12-31 11:21:50',11,'2018-12-31 11:31:33',11),(2,'TEST','region test',2,1,1,'2018-12-31 11:33:07',11,'2018-12-31 11:33:15',11),(3,'ALC','alaska',4,1,0,'2019-01-02 03:18:27',11,NULL,NULL);
/*!40000 ALTER TABLE `region` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `retailer`
--

DROP TABLE IF EXISTS `retailer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `retailer` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_account_id` int(11) DEFAULT NULL,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `image_url` varchar(100) CHARACTER SET latin1 DEFAULT NULL,
  `is_company` tinyint(4) NOT NULL DEFAULT '0',
  `tax_code` varchar(15) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `contact_id` int(11) DEFAULT NULL,
  `address_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retailer`
--

LOCK TABLES `retailer` WRITE;
/*!40000 ALTER TABLE `retailer` DISABLE KEYS */;
INSERT INTO `retailer` VALUES (11,19,'Công ty TNHH Retailer1','/Images/Retailer/201901/58d167664d7147f2af18613c7af72289.jpeg',1,'0123456789',12,11,1,0,'2019-01-03 06:40:54',11,'2019-01-11 07:51:36',11);
/*!40000 ALTER TABLE `retailer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `retailer_buying_calendar`
--

DROP TABLE IF EXISTS `retailer_buying_calendar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `retailer_buying_calendar` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `retailer_id` int(11) NOT NULL,
  `buying_date` date NOT NULL,
  `is_ordered` tinyint(4) NOT NULL DEFAULT '0',
  `is_expired` tinyint(4) NOT NULL DEFAULT '0',
  `is_adaped` tinyint(4) NOT NULL,
  `adap_note` varchar(1024) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_retailer_buying_calendar_retailer_idx` (`retailer_id`),
  CONSTRAINT `pk_retailer_buying_calendar_retailer` FOREIGN KEY (`retailer_id`) REFERENCES `retailer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retailer_buying_calendar`
--

LOCK TABLES `retailer_buying_calendar` WRITE;
/*!40000 ALTER TABLE `retailer_buying_calendar` DISABLE KEYS */;
INSERT INTO `retailer_buying_calendar` VALUES (1,'P000000002','Planning test 1',11,'2019-01-30',0,0,1,'',0,'2019-01-02 17:00:00',11,'2019-01-14 08:08:50',11),(4,'P000000001','Planning test history',11,'2018-01-30',1,0,1,' ',0,'2019-01-02 17:00:00',11,NULL,NULL),(7,'P000000003','Planning Test Add 1',11,'2019-01-16',0,0,1,'',0,'2019-01-14 06:46:42',19,'2019-01-14 08:09:00',11),(8,'P000000004','Planning Test Add 2',11,'2019-01-16',1,0,1,'',0,'2019-01-14 06:54:54',19,'2019-01-14 11:39:14',19);
/*!40000 ALTER TABLE `retailer_buying_calendar` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `retailer_buying_calendar_item`
--

DROP TABLE IF EXISTS `retailer_buying_calendar_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `retailer_buying_calendar_item` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `retailer_buying_calendar_id` bigint(20) NOT NULL,
  `product_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `adap_quantity` int(11) NOT NULL,
  `uom_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_retailer_buying_calendar_retailer_buying_calendar_item_idx` (`retailer_buying_calendar_id`),
  KEY `pk_retailer_buying_calendar_item_product_idx` (`product_id`),
  KEY `pk_retailer_buying_calendar_item_uom_idx` (`uom_id`),
  CONSTRAINT `pk_retailer_buying_calendar_item_product` FOREIGN KEY (`product_id`) REFERENCES `product` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_retailer_buying_calendar_item_uom` FOREIGN KEY (`uom_id`) REFERENCES `uom` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_retailer_buying_calendar_retailer_buying_calendar_item` FOREIGN KEY (`retailer_buying_calendar_id`) REFERENCES `retailer_buying_calendar` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retailer_buying_calendar_item`
--

LOCK TABLES `retailer_buying_calendar_item` WRITE;
/*!40000 ALTER TABLE `retailer_buying_calendar_item` DISABLE KEYS */;
INSERT INTO `retailer_buying_calendar_item` VALUES (5,4,6,100,100,1),(6,4,10,2000,2000,2),(11,1,6,100,100,1),(12,1,10,20,20,2),(15,8,4,123,123,2),(16,7,4,312312,312312,1);
/*!40000 ALTER TABLE `retailer_buying_calendar_item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `retailer_location`
--

DROP TABLE IF EXISTS `retailer_location`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `retailer_location` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `gln` varchar(50) NOT NULL,
  `name` varchar(50) NOT NULL,
  `description` varchar(255) NOT NULL,
  `image_url` varchar(100) DEFAULT NULL,
  `retailer_id` int(11) NOT NULL,
  `contact_id` int(11) NOT NULL,
  `address_id` int(11) NOT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retailer_location`
--

LOCK TABLES `retailer_location` WRITE;
/*!40000 ALTER TABLE `retailer_location` DISABLE KEYS */;
INSERT INTO `retailer_location` VALUES (1,'Retailer1Location1','Location 1 - retailer1','Location 1 - retailer1','/Images/Retailer/201901/6169aae7053c461487d5547aabfa8573.jpeg',11,13,12,1,0,'2019-01-03 08:41:51',11,'2019-01-17 04:16:19',19);
/*!40000 ALTER TABLE `retailer_location` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `retailer_order`
--

DROP TABLE IF EXISTS `retailer_order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `retailer_order` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `distribution_id_to` int(11) DEFAULT NULL,
  `trip_id` int(11) DEFAULT NULL,
  `retailer_id` int(11) NOT NULL,
  `retailer_buying_calendar_id` bigint(20) DEFAULT NULL,
  `status_id` int(11) NOT NULL,
  `buying_date` date NOT NULL,
  `bill_to` int(11) NOT NULL,
  `ship_to` int(11) NOT NULL,
  `total_amount` decimal(10,0) NOT NULL,
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  `retailer_ordercol` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_retailer_order_retailer_idx` (`retailer_id`),
  KEY `pk_retailer_order_retailer_buying_calendar_idx` (`retailer_buying_calendar_id`),
  KEY `pk_retailer_order_retailer_order_status_idx` (`status_id`),
  KEY `pk_retailer_order_retailer_location_bill_to_idx` (`bill_to`),
  KEY `pk_retailer_order_retailer_location_ship_to_idx` (`ship_to`),
  CONSTRAINT `pk_retailer_order_retailer` FOREIGN KEY (`retailer_id`) REFERENCES `retailer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_retailer_order_retailer_buying_calendar` FOREIGN KEY (`retailer_buying_calendar_id`) REFERENCES `retailer_buying_calendar` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_retailer_order_retailer_location_bill_to` FOREIGN KEY (`bill_to`) REFERENCES `retailer_location` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_retailer_order_retailer_location_ship_to` FOREIGN KEY (`ship_to`) REFERENCES `retailer_location` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_retailer_order_retailer_order_status` FOREIGN KEY (`status_id`) REFERENCES `retailer_order_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retailer_order`
--

LOCK TABLES `retailer_order` WRITE;
/*!40000 ALTER TABLE `retailer_order` DISABLE KEYS */;
INSERT INTO `retailer_order` VALUES (1,'O201900001','RO00000001',2,NULL,11,4,7,'2019-01-07',1,1,8000,0,'2019-01-03 17:00:00',11,NULL,NULL,NULL),(2,'O201900002','RO00000002',2,3,11,NULL,6,'2019-01-07',1,1,8000,0,'2019-01-03 17:00:00',11,'2019-01-22 10:40:23',26,NULL),(6,'O201900003','Đơn hàng 1',2,NULL,11,8,-1,'2019-01-16',1,1,28810782,0,'2019-01-14 11:39:14',19,'2019-01-16 08:19:52',11,NULL);
/*!40000 ALTER TABLE `retailer_order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `retailer_order_audit`
--

DROP TABLE IF EXISTS `retailer_order_audit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `retailer_order_audit` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `retailer_id` int(11) NOT NULL,
  `retailer_order_id` bigint(20) NOT NULL,
  `retailer_order_item_id` bigint(20) DEFAULT NULL,
  `status_id` int(11) NOT NULL,
  `note` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_audit_retailer_idx` (`retailer_id`),
  KEY `fk_audit_retailer_order_idx` (`retailer_order_id`),
  KEY `fk_audit_retailer_order_status_idx` (`status_id`),
  KEY `fk_audit_retailer_order_item_idx` (`retailer_order_item_id`),
  CONSTRAINT `fk_audit_retailer` FOREIGN KEY (`retailer_id`) REFERENCES `retailer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_audit_retailer_order` FOREIGN KEY (`retailer_order_id`) REFERENCES `retailer_order` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_audit_retailer_order_item` FOREIGN KEY (`retailer_order_item_id`) REFERENCES `retailer_order_item` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_audit_retailer_order_status` FOREIGN KEY (`status_id`) REFERENCES `retailer_order_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retailer_order_audit`
--

LOCK TABLES `retailer_order_audit` WRITE;
/*!40000 ALTER TABLE `retailer_order_audit` DISABLE KEYS */;
INSERT INTO `retailer_order_audit` VALUES (1,11,2,NULL,2,'','2019-01-16 11:04:44',11),(5,11,2,3,3,NULL,'2019-01-16 12:04:44',11),(6,11,2,4,3,NULL,'2019-01-16 12:04:44',11);
/*!40000 ALTER TABLE `retailer_order_audit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `retailer_order_item`
--

DROP TABLE IF EXISTS `retailer_order_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `retailer_order_item` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `retailer_order_id` bigint(20) NOT NULL,
  `product_id` int(11) NOT NULL,
  `status_id` int(11) NOT NULL,
  `price` decimal(10,0) NOT NULL,
  `ordered_quantity` int(11) NOT NULL,
  `adap_quantity` int(11) DEFAULT NULL,
  `deliveried_quantity` int(11) DEFAULT NULL,
  `uom_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_retailer_order_item_retailer_order_idx` (`retailer_order_id`),
  KEY `pk_retailer_order_item_product_idx` (`product_id`),
  KEY `pk_retailer_order_uom_idx` (`uom_id`),
  KEY `pk_retailer_order_item_retailer_order_status_idx` (`status_id`),
  CONSTRAINT `pk_retailer_order_item_product` FOREIGN KEY (`product_id`) REFERENCES `product` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_retailer_order_item_retailer_order` FOREIGN KEY (`retailer_order_id`) REFERENCES `retailer_order` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_retailer_order_item_retailer_order_status` FOREIGN KEY (`status_id`) REFERENCES `retailer_order_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_retailer_order_uom` FOREIGN KEY (`uom_id`) REFERENCES `uom` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retailer_order_item`
--

LOCK TABLES `retailer_order_item` WRITE;
/*!40000 ALTER TABLE `retailer_order_item` DISABLE KEYS */;
INSERT INTO `retailer_order_item` VALUES (1,1,1,7,30,100,100,100,1),(2,1,2,7,5,1000,1000,1000,2),(3,2,1,2,30,100,0,0,1),(4,2,2,2,5,1000,0,0,2),(11,6,4,2,234234,123,0,0,2);
/*!40000 ALTER TABLE `retailer_order_item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `retailer_order_status`
--

DROP TABLE IF EXISTS `retailer_order_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `retailer_order_status` (
  `id` int(11) NOT NULL,
  `caption_name` varchar(50) NOT NULL,
  `caption_description` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retailer_order_status`
--

LOCK TABLES `retailer_order_status` WRITE;
/*!40000 ALTER TABLE `retailer_order_status` DISABLE KEYS */;
INSERT INTO `retailer_order_status` VALUES (-1,'RetailerStatus.Name.Canceled','RetailerStatus.Description.Canceled'),(1,'RetailerStatus.Name.Ordered','RetailerStatus.Description.Ordered'),(2,'RetailerStatus.Name.ComfirmedOrder','RetailerStatus.Description.ComfirmedOrder'),(3,'RetailerStatus.Name.FarmerOrdered','RetailerStatus.Description.FarmerOrdered'),(4,'RetailerStatus.Name.InConllections','RetailerStatus.Description.InConllections'),(5,'RetailerStatus.Name.InFulfillment','RetailerStatus.Description.InFulfillment'),(6,'RetailerStatus.Name.InLogistic','RetailerStatus.Description.InLogistic'),(7,'RetailerStatus.Name.Completed','RetailerStatus.Description.Completed');
/*!40000 ALTER TABLE `retailer_order_status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `id` smallint(6) NOT NULL,
  `name` varchar(50) CHARACTER SET utf8 NOT NULL,
  `is_external_role` tinyint(4) NOT NULL DEFAULT '0',
  `description` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Farmer',1,'Người nông dân',1),(2,'Retailer',1,'Nhà bán lẻ',1),(3,'Administrator',0,'Quản trị viên cao cấp',1),(4,'QCSupervisor',0,'Giám sát đội ngũ quản lý chất lượng sản phẩm',1),(5,'QCStaff',0,'Nhân viên quản lý chất lượng sản phẩm',1),(6,'Collector',0,'Nhân viên thu mua sản phẩm',1),(7,'Fulfillmentor',0,'Nhân viên xử lý sản phẩm',1),(8,'DeliverySupervisor',0,'Giám sát đội ngũ chuyển hàng',1),(9,'DeliveryMan',0,'Nhân viên chuyển hàng',1),(10,'DeliveryDriver',0,'Nhân viên lái xe chuyển hàng',1),(11,'SalesSupervisor',0,'Giám sát đội ngũ bán hàng',1),(12,'Saller',0,'Nhân viên bán hàng',1);
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `router`
--

DROP TABLE IF EXISTS `router`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `router` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `description` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `current_longitude` float NOT NULL,
  `current_latitude` float NOT NULL,
  `radius` float NOT NULL,
  `distribution_id` int(11) NOT NULL,
  `country_id` int(11) NOT NULL,
  `province_id` int(11) NOT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_router_distribution_idx` (`distribution_id`),
  CONSTRAINT `fk_router_distribution` FOREIGN KEY (`distribution_id`) REFERENCES `distribution` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `router`
--

LOCK TABLES `router` WRITE;
/*!40000 ALTER TABLE `router` DISABLE KEYS */;
INSERT INTO `router` VALUES (3,'TPHCM - Q1','Khu vực TPHCM- Bến thành- quận 1',106.74,10.799,15136.8,2,2,1,1,0,'2019-01-23 04:07:32',26,'2019-01-30 02:17:09',26),(4,'Router2','Router2',106.576,10.6676,13257.8,2,2,1,1,0,'2019-01-23 04:07:32',26,'2019-01-30 02:17:33',26),(5,'Router3','router3',106.566,10.8121,5000,2,2,1,1,0,'2019-01-23 04:07:32',26,'2019-01-30 02:17:19',26),(6,'Router4','Router4',106.65,10.7121,5000,2,2,1,1,1,'2019-01-23 04:07:32',26,'2019-01-18 11:14:47',26),(7,'Router5','router5',106.605,10.976,15656.1,2,2,1,1,0,'2019-01-23 04:07:32',26,'2019-01-18 11:09:31',26);
/*!40000 ALTER TABLE `router` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `trip`
--

DROP TABLE IF EXISTS `trip`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `trip` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `router_id` int(11) DEFAULT NULL,
  `distribution_id` int(11) NOT NULL,
  `status_id` smallint(6) NOT NULL,
  `deliveryman_id` int(11) DEFAULT NULL,
  `driver_id` int(11) DEFAULT NULL,
  `vehicle_id` int(11) DEFAULT NULL,
  `current_longitude` float DEFAULT NULL,
  `current_latitude` float DEFAULT NULL,
  `delivery_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_trip_status_idx` (`status_id`),
  KEY `fk_trip_employee_idx` (`deliveryman_id`),
  KEY `fk_trip_driver_idx` (`driver_id`),
  KEY `fk_trip_vehicle_idx` (`vehicle_id`),
  KEY `fk_trip_route_idx` (`router_id`),
  KEY `fk_trip_distribution_idx` (`distribution_id`),
  CONSTRAINT `fk_trip_deliveryman` FOREIGN KEY (`deliveryman_id`) REFERENCES `employee` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_trip_distribution` FOREIGN KEY (`distribution_id`) REFERENCES `distribution` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_trip_driver` FOREIGN KEY (`driver_id`) REFERENCES `employee` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_trip_route` FOREIGN KEY (`router_id`) REFERENCES `router` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_trip_status` FOREIGN KEY (`status_id`) REFERENCES `trip_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_trip_vehicle` FOREIGN KEY (`vehicle_id`) REFERENCES `vehicle` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trip`
--

LOCK TABLES `trip` WRITE;
/*!40000 ALTER TABLE `trip` DISABLE KEYS */;
INSERT INTO `trip` VALUES (2,'T201900001',3,2,2,5,6,1,0,0,'2019-01-23 04:05:44',0,'2019-01-20 17:00:00',11,'2019-01-23 04:21:29',26),(3,'T201900002',3,2,4,5,6,1,106.695,10.839,'2019-01-23 04:06:21',0,'2019-01-22 10:05:30',26,'2019-01-22 10:42:40',26),(4,'T201900003',4,2,2,5,6,1,0,0,'2019-01-27 17:00:00',0,'2019-01-29 03:44:58',26,'2019-01-29 03:45:13',26),(5,'T201900004',3,2,1,5,6,1,0,0,'2019-01-29 17:00:00',0,'2019-01-29 07:42:48',26,NULL,NULL);
/*!40000 ALTER TABLE `trip` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `trip_audit`
--

DROP TABLE IF EXISTS `trip_audit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `trip_audit` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `trip_id` int(11) NOT NULL,
  `status_id` smallint(6) DEFAULT NULL,
  `longitude` float NOT NULL,
  `latitude` float NOT NULL,
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_trip_audit_trip_status_idx` (`status_id`),
  KEY `fk_trip_audit_trip_idx` (`trip_id`),
  CONSTRAINT `fk_trip_audit_trip` FOREIGN KEY (`trip_id`) REFERENCES `trip` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_trip_audit_trip_status` FOREIGN KEY (`status_id`) REFERENCES `trip_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trip_audit`
--

LOCK TABLES `trip_audit` WRITE;
/*!40000 ALTER TABLE `trip_audit` DISABLE KEYS */;
/*!40000 ALTER TABLE `trip_audit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `trip_order`
--

DROP TABLE IF EXISTS `trip_order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `trip_order` (
  `id` bigint(20) NOT NULL,
  `trip_id` int(11) NOT NULL,
  `order_id` bigint(20) NOT NULL,
  `is_completed` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `fk_trip_order_trip_idx` (`trip_id`),
  CONSTRAINT `fk_trip_order_trip` FOREIGN KEY (`trip_id`) REFERENCES `trip` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trip_order`
--

LOCK TABLES `trip_order` WRITE;
/*!40000 ALTER TABLE `trip_order` DISABLE KEYS */;
/*!40000 ALTER TABLE `trip_order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `trip_status`
--

DROP TABLE IF EXISTS `trip_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `trip_status` (
  `id` smallint(6) NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `description` varchar(256) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `flag_color` varchar(7) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id`,`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trip_status`
--

LOCK TABLES `trip_status` WRITE;
/*!40000 ALTER TABLE `trip_status` DISABLE KEYS */;
INSERT INTO `trip_status` VALUES (-1,'Canceled','Hủy chuyến','#FF0000'),(1,'Created','Đã tạo','#D7D7D7'),(2,'OnConfirmed','Đã xác nhận chuyến','#0000FF'),(3,'OnTriped','Đang giao hàng','#00FFFF'),(4,'Finished','Hoàn thành','#00FF00');
/*!40000 ALTER TABLE `trip_status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uom`
--

DROP TABLE IF EXISTS `uom`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `uom` (
  `id` int(11) NOT NULL,
  `code` varchar(10) CHARACTER SET latin1 NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `description` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uom`
--

LOCK TABLES `uom` WRITE;
/*!40000 ALTER TABLE `uom` DISABLE KEYS */;
INSERT INTO `uom` VALUES (1,'THUNG','Thùng',NULL,1,0,'2018-12-26 17:00:00',11,NULL,NULL),(2,'CHAI','Chai',NULL,1,0,'2018-12-26 17:00:00',11,NULL,NULL),(3,'LON','Lon','Đơn vị tính lon',1,1,'2019-01-07 07:37:38',11,'2019-01-07 07:56:35',11),(4,'HOP','Hộp','Đơn vị hộp',1,0,'2019-01-07 08:16:28',11,NULL,NULL);
/*!40000 ALTER TABLE `uom` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_access_token`
--

DROP TABLE IF EXISTS `user_access_token`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_access_token` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `access_token` varchar(256) CHARACTER SET utf8 NOT NULL,
  `login_date` datetime DEFAULT NULL,
  `expired_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_user_access_token_user_account_idx` (`user_id`),
  CONSTRAINT `pk_user_access_token_user_account` FOREIGN KEY (`user_id`) REFERENCES `user_account` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=267 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_access_token`
--

LOCK TABLES `user_access_token` WRITE;
/*!40000 ALTER TABLE `user_access_token` DISABLE KEYS */;
INSERT INTO `user_access_token` VALUES (262,11,'v/8L7W6/hlAkX8r36xdkRgmPMwrxMT09IjNOwl2qame06BnkdQshAk3STcnBeAJN','2019-01-31 09:25:30','9999-12-31 23:59:59'),(263,11,'v/8L7W6/hlAkX8r36xdkRvF7QXiPRFpRptQepzOY94a06BnkdQshAk3STcnBeAJN','2019-01-31 09:35:52','9999-12-31 23:59:59'),(264,29,'nTufj2wEWNoPrf8eGWZjhwniZGpCD4FWSpeQs3ydTzJ94wTBEErL5bTRT2KFwpvAbmSzRt1Egd4=','2019-01-31 09:40:17','9999-12-31 23:59:59'),(265,11,'v/8L7W6/hlAkX8r36xdkRtCX7mN08T1eoguRLluHXyG06BnkdQshAk3STcnBeAJN','2019-01-31 11:52:16','9999-12-31 23:59:59'),(266,26,'UxnyhthnBwdTBDadFeRMnmT/FKZ8fjb03Ai1WcoJkv+Gr5FSmOWzFHsdTPaJQjBF+rwD0ccheDc=','2019-01-31 16:29:50','9999-12-31 23:59:59');
/*!40000 ALTER TABLE `user_access_token` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_account`
--

DROP TABLE IF EXISTS `user_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_account` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(50) CHARACTER SET utf8 NOT NULL,
  `password` varchar(50) CHARACTER SET utf8 NOT NULL,
  `email` varchar(100) CHARACTER SET utf8 NOT NULL,
  `security_password` char(36) NOT NULL,
  `password_reset_code` char(8) DEFAULT NULL,
  `is_external_user` tinyint(4) DEFAULT '1',
  `is_superadmin` tinyint(4) NOT NULL DEFAULT '0',
  `is_deleted` tinyint(4) DEFAULT '0',
  `is_actived` tinyint(4) NOT NULL DEFAULT '0',
  `is_used` tinyint(4) DEFAULT '1',
  `created_by` int(11) DEFAULT NULL,
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_account`
--

LOCK TABLES `user_account` WRITE;
/*!40000 ALTER TABLE `user_account` DISABLE KEYS */;
INSERT INTO `user_account` VALUES (10,'hoangnguyen','D7023BAD32181EF8A7FF7FA171399D70','hoangnguyen@aritnt.com.vn','019e5ce5-2a8c-48ea-a5f7-160626d0fa44',NULL,0,0,0,1,1,NULL,'2018-12-18 11:45:08'),(11,'admin','D7023BAD32181EF8A7FF7FA171399D70','admin@gmail.com','019e5ce5-2a8c-48ea-a5f7-160626d0fa44',NULL,0,0,0,1,1,NULL,'2018-12-19 05:15:22'),(16,'haula','517A81A408BC741830713A0A5BA1FA80','lahau@gmail.com','09a223b1-bc21-42fe-b807-dcd81dccb9e1',NULL,0,0,0,1,1,NULL,'2018-12-19 05:33:51'),(17,'supervisorKhoi','B023B537E8AC3BF091A1472AC733D6DE','vankhoi@gmail.com','7de1f578-9c06-42ff-8c71-27126679bb69',NULL,0,0,0,1,1,11,'2018-12-19 05:34:51'),(18,'farmer1','A4D7FD8C0D1FCC92F4E7D0F538B8E05E','farmer1@aritnt.com.vn','2d93d43a-bae2-4fb8-965e-aaa7bcfba719',NULL,1,0,0,1,1,11,'2019-01-03 02:44:15'),(19,'retailer1','4135B000D6B67B0A34DBD12325E5CAC9','retailer1@aritnt.com.vn','6f501153-512b-42c9-88a5-10a058cbc154',NULL,1,0,0,1,1,11,'2019-01-03 08:53:31'),(20,'QCHoang','8A1C224C4C5C2529D2B13F430ED8E460','hoangnguyen@aritnt.com.vn','5d24b60d-72a0-4a95-a3d1-0bdeb558fb1c',NULL,0,0,0,1,1,11,'2019-01-09 08:58:33'),(21,'farmer2','B0A8D9C5546E71BAED2939B346B49C3C','farmer2@aritnt.com.vn','d90aa235-f612-4b1d-9c62-9ccfd2ed23d9',NULL,1,0,0,1,1,11,'2019-01-10 07:52:23'),(22,'Nguyễn Huy Hoàng','7A5A5229F504372DA209691B026D9508','hoang@aritnt.com.vn','d21885ce-2a41-4b51-9c33-e0301fa34475',NULL,1,0,0,1,1,11,'2019-01-10 09:00:37'),(23,'Lã Thị Hậu','FA13FC29045AA87638DF14A6474FDA8F','hau@aritnt.com.vn','03b1fffe-42c0-4eaa-907f-e5f7039e2909',NULL,0,0,0,0,1,11,'2019-01-10 09:09:28'),(24,'0338061306','843C54C165F2114790718FB04A40CDA5','hau@aritnt.com.vn','f8aa71b7-8290-4a03-b208-46475b325f18',NULL,0,0,0,0,1,11,'2019-01-11 03:01:58'),(25,'Anh','B7DBBF9416E4A6C13A278884DE2AF428','anh@gmail.com.vn','ca9c0705-7876-4d48-ba67-739289b0e7f0',NULL,0,0,0,0,1,11,'2019-01-11 03:05:57'),(26,'distributor','ED38CD22ED8D7665355270C4C8816FD9','distributor@aritnt.com.vn','ddc0fe9e-8823-4b68-a6d0-1413025c43c7',NULL,0,0,0,1,1,11,'2019-01-17 07:52:06'),(27,'deliveryman','1DA006BA9B1E31BD2997F518CF74510B','deliveryman@aritnt.com.vn','7fea628e-e94c-4b1c-8715-4c74dcaec95d',NULL,0,0,0,1,1,11,'2019-01-21 09:58:47'),(28,'driver','1EE33172F62CC42A79CF96F7DA47B3DE','driver@aritnt.com.vn','80729ed4-2bb5-4339-b4a8-19f989ec54d2',NULL,0,0,0,1,1,11,'2019-01-21 10:02:35'),(29,'collector','FE5C9E08C6F78BFA56B28C2654F32A46','collector@aritnt.com.vn','0e92ff87-65a4-4357-95c9-d3ccb4be7f5a',NULL,0,0,0,1,1,11,'2019-01-29 10:25:55'),(30,'haulaa','9DC196F6C3B425FFDD9ED2A746670C91','lahauit@gmail.com.vn','308fde68-6650-458d-a632-c1e3b0e004a3',NULL,0,0,0,0,1,11,'2019-01-31 01:52:06'),(31,'lahau@@@@','89AA006B09316D1D8B6DABEE92B0FD14','haula@arit.com.vn','cca87dd4-61b8-4dfa-8cf4-582102d33ddb',NULL,0,0,0,0,1,11,'2019-01-31 01:55:42'),(32,'lathihau','C32A91DB64F2EBDC1265536DDA59FF80','mail1@aritnt.com.vn','65f69ce4-698a-4825-bf53-a35e8630de14',NULL,0,0,0,0,1,11,'2019-01-31 02:00:18'),(33,'hau2','D97DB261F2DCBB56FBDFF6203C3921C5','hau2@arritnt.com.vn','f677b0c8-65ed-4794-abca-b07797c7f0a7',NULL,0,0,0,0,1,11,'2019-01-31 02:04:50'),(34,'hau3','243EAEE331945B8D45A4960C9BB0A596','hau3@aritnt.com.vn','b7ab94aa-1a66-4d7d-be81-f9721be3ff31',NULL,0,0,0,0,1,11,'2019-01-31 03:12:16'),(35,'hau4','17B07A59DCD6057BEA026C26752F0FFD','lahau@gmail.com','a4f8454c-71a9-4802-8601-69b9bc76aa43',NULL,0,0,0,0,1,11,'2019-01-31 09:08:55');
/*!40000 ALTER TABLE `user_account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_account_role`
--

DROP TABLE IF EXISTS `user_account_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_account_role` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user_account_id` int(11) NOT NULL,
  `role_id` smallint(6) NOT NULL,
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `id_idx` (`user_account_id`),
  KEY `pk_user_account_role_role_idx` (`role_id`),
  CONSTRAINT `pk_user_account_role_role` FOREIGN KEY (`role_id`) REFERENCES `role` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_user_account_role_user_account` FOREIGN KEY (`user_account_id`) REFERENCES `user_account` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_account_role`
--

LOCK TABLES `user_account_role` WRITE;
/*!40000 ALTER TABLE `user_account_role` DISABLE KEYS */;
INSERT INTO `user_account_role` VALUES (8,17,4,'2018-12-19 05:33:53'),(9,18,1,'2018-12-19 05:33:53'),(10,19,2,'2018-12-19 05:33:53'),(19,20,5,'2019-01-09 08:58:53'),(20,21,1,'2019-01-10 07:55:02'),(21,22,1,'2019-01-10 09:03:26'),(24,16,5,'2019-01-11 03:07:25'),(25,10,3,'2019-01-17 07:45:49'),(26,10,4,'2019-01-17 07:45:49'),(27,10,8,'2019-01-17 07:45:49'),(28,26,8,'2019-01-17 07:52:16'),(29,11,3,'2019-01-18 04:15:38'),(30,23,4,'2019-01-18 04:34:46'),(31,23,5,'2019-01-18 04:34:46'),(32,23,10,'2019-01-18 04:34:46'),(33,27,9,'2019-01-21 10:01:46'),(34,28,10,'2019-01-21 10:02:54'),(35,29,6,'2019-01-29 10:26:08');
/*!40000 ALTER TABLE `user_account_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vehicle`
--

DROP TABLE IF EXISTS `vehicle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `vehicle` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `image_url` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `weight` int(11) NOT NULL,
  `capacity` int(11) NOT NULL,
  `type_id` smallint(6) NOT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_vehicle_vehicle_type_idx` (`type_id`),
  CONSTRAINT `fk_vehicle_vehicle_type` FOREIGN KEY (`type_id`) REFERENCES `vehicle_type` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehicle`
--

LOCK TABLES `vehicle` WRITE;
/*!40000 ALTER TABLE `vehicle` DISABLE KEYS */;
INSERT INTO `vehicle` VALUES (1,'Xe loại 1','/Images/Vehicle/201901/e41af00eb6bb48a2ab78053e288c5056.jpeg',1500,3000,2,1,0,'2019-01-10 03:58:34',11,'2019-01-17 03:55:18',11);
/*!40000 ALTER TABLE `vehicle` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vehicle_type`
--

DROP TABLE IF EXISTS `vehicle_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `vehicle_type` (
  `id` smallint(6) NOT NULL,
  `name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehicle_type`
--

LOCK TABLES `vehicle_type` WRITE;
/*!40000 ALTER TABLE `vehicle_type` DISABLE KEYS */;
INSERT INTO `vehicle_type` VALUES (2,'Loại 2',1),(3,'Loại 3',1),(4,'Loại 4',1),(5,'Loại 5',1),(6,'Loại 6',1);
/*!40000 ALTER TABLE `vehicle_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ward`
--

DROP TABLE IF EXISTS `ward`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ward` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET latin1 NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `country_id` int(11) NOT NULL,
  `province_id` int(11) NOT NULL,
  `district_id` int(11) NOT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_ward_country_idx` (`country_id`),
  KEY `pk_ward_province_idx` (`province_id`),
  KEY `pk_ward_district_idx` (`district_id`),
  CONSTRAINT `pk_ward_country` FOREIGN KEY (`country_id`) REFERENCES `country` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_ward_district` FOREIGN KEY (`district_id`) REFERENCES `district` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_ward_province` FOREIGN KEY (`province_id`) REFERENCES `province` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ward`
--

LOCK TABLES `ward` WRITE;
/*!40000 ALTER TABLE `ward` DISABLE KEYS */;
INSERT INTO `ward` VALUES (1,'F1','Phường 1',2,1,1,1,0,'2019-01-02 04:59:27',11,'2019-01-02 05:05:33',11);
/*!40000 ALTER TABLE `ward` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-01-31 16:32:49
