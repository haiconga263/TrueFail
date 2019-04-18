-- MySQL dump 10.13  Distrib 5.5.62, for Win64 (AMD64)
--
-- Host: localhost    Database: aritrace
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
  `object_type` varchar(1) CHARACTER SET latin1 DEFAULT NULL,
  `object_id` int(11) DEFAULT NULL COMMENT 'The object id is a id of object reference',
  `street` varchar(100) CHARACTER SET utf8 DEFAULT '',
  `country_id` int(11) DEFAULT NULL,
  `province_id` int(11) DEFAULT NULL,
  `district_id` int(11) DEFAULT NULL,
  `ward_id` int(11) DEFAULT NULL,
  `longitude` float DEFAULT NULL,
  `latitude` float DEFAULT NULL,
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_address_country_idx` (`country_id`),
  KEY `pk_address_province_idx` (`province_id`),
  KEY `pk_address_district_idx` (`district_id`),
  KEY `pk_address_ward_idx` (`ward_id`),
  CONSTRAINT `pk_address_country` FOREIGN KEY (`country_id`) REFERENCES `country` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_address_district` FOREIGN KEY (`district_id`) REFERENCES `district` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_address_province` FOREIGN KEY (`province_id`) REFERENCES `province` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_address_ward` FOREIGN KEY (`ward_id`) REFERENCES `ward` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `address`
--

LOCK TABLES `address` WRITE;
/*!40000 ALTER TABLE `address` DISABLE KEYS */;
INSERT INTO `address` VALUES (1,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-04 06:01:01',11,'2019-01-04 06:01:01',11),(2,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-04 06:02:21',11,'2019-01-04 06:02:21',11),(3,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-04 06:08:38',11,'2019-01-04 06:08:38',11),(4,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-04 06:09:59',11,'2019-01-04 06:09:59',11),(5,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-04 06:10:26',11,'2019-01-04 06:10:26',11),(6,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,2,6,NULL,NULL,0,'2019-01-04 06:43:57',11,'2019-01-04 06:43:57',11),(7,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,2,6,NULL,NULL,0,'2019-01-04 06:51:53',11,'2019-01-04 06:51:53',11),(8,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,2,6,NULL,NULL,0,'2019-01-04 06:55:50',11,'2019-01-04 06:55:50',11),(9,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-11 11:27:58',11,'2019-01-11 11:27:58',11),(10,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-11 11:28:34',11,'2019-01-11 11:28:34',11),(11,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-11 11:38:23',11,'2019-01-11 11:38:23',11),(12,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-11 11:42:16',11,'2019-01-11 11:42:16',11),(13,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-11 11:47:07',11,'2019-01-11 11:47:07',11),(14,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-11 11:48:05',11,'2019-01-11 11:48:05',11),(15,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-11 11:49:16',11,'2019-01-11 11:49:16',11),(16,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-11 11:54:45',11,'2019-01-11 11:54:45',11),(17,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-11 13:00:12',11,'2019-01-11 13:00:12',11),(18,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-12 12:04:50',11,'2019-01-12 12:04:50',11),(19,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-12 12:04:57',11,'2019-01-12 12:04:57',11),(20,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-12 12:13:00',11,'2019-01-12 12:13:00',11),(21,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-12 12:13:07',11,'2019-01-12 12:13:07',11),(22,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-12 12:18:19',11,'2019-01-12 12:18:19',11),(23,NULL,NULL,'Số 17 phố Duy Tân, Phường Dịch Vọng Hậu, Quận Cầu Giấy, Thành phố Hà Nội, Việt Nam',1,1,1,1,NULL,NULL,0,'2019-01-12 12:50:39',11,'2019-01-12 12:50:39',11);
/*!40000 ALTER TABLE `address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `caption`
--

DROP TABLE IF EXISTS `caption`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `caption` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) DEFAULT NULL,
  `type` smallint(6) NOT NULL DEFAULT '0' COMMENT 'type is a column that decribe type of caption.\\\\\\\\n1/ Messgae\\\\\\\\n2/ Lable\\\\\\\\n3/ Column',
  `default_caption` varchar(4000) NOT NULL,
  `is_common` tinyint(4) NOT NULL DEFAULT '1',
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `caption`
--

LOCK TABLES `caption` WRITE;
/*!40000 ALTER TABLE `caption` DISABLE KEYS */;
INSERT INTO `caption` VALUES (1,'NotExistedAccount',1,'The account is not exist',1,1,0,'2019-01-09 17:00:00',0,'2019-01-14 12:18:14',11),(2,'WrongPasswordAccount',1,'Password is wrong',1,1,0,'2019-01-09 17:00:00',0,'2019-01-15 03:08:35',11),(3,'NotActivedAccount',1,'Tài khoản chưa được kích hoạt',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(4,'LockedAccount',1,'Tài khoản đã bị khóa',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(5,'ActivedAccount',1,'Tài khoản đã được kích hoạt rồi',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(6,'ExternalAccount',1,'Tài khoản này là tài khoản external',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(7,'NotEnoughConditionResetPassword',1,'Tài khoản không đủ điều kiện để reset mật khẩu',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(8,'Product.DeletedFailed',1,'Xóa sản phẩm thất bại',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(9,'Image.OutOfLength',1,'Dung lượng hình ảnh quá lớn',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(10,'Image.WrongType',1,'Hình ảnh không đúng kiểu',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(11,'Employee.NotExisted',1,'Nhân viên không tồn tại',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(12,'AddWrongInformation',1,'Thông tin khởi tạo không đúng',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(13,'Employee.ExistedCode',1,'Mã nhân viên đã được sử dụng',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(14,'Product.NotExisted',1,'Sản phẩm không tồn tại',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(15,'Product.ExistedCode',1,'Mã sản phẩm đã được sử dụng',1,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(23,'Category.Fish',0,'Fish',1,1,0,'2019-01-15 10:50:34',11,'2019-01-15 10:52:08',11),(24,'Category.Meat',0,'Meat',1,1,0,'2019-01-15 10:51:48',11,'2019-01-15 10:51:48',11),(25,'Category.FruitVegetable',0,'Fruit & Vegetable',1,1,0,'2019-01-15 10:52:44',11,'2019-01-15 10:52:44',11),(26,'CompanyType.TheManufacturerSupplier',0,'The manufacturer or supplier',1,1,0,'2019-01-16 03:34:30',11,'2019-01-16 03:34:30',11),(27,'CompanyType.TheManufacturerSupplier',0,'The manufacturer or supplier',1,1,0,'2019-01-16 03:36:17',11,'2019-01-16 03:36:17',11),(28,'CompanyType.TheImporterWholesaler',0,'The importer or wholesaler',1,1,0,'2019-01-16 03:39:16',11,'2019-01-16 03:40:02',11),(29,'CompanyType.TheRetailer',0,'The retailer',1,1,0,'2019-01-16 03:40:55',11,'2019-01-16 04:17:40',11),(30,'CompanyType.Other',0,'Other',1,1,0,'2019-01-16 04:17:15',11,'2019-01-16 04:17:34',11),(31,'GrowingMethod.VietGAPStandard',0,'VietGAP Standard',1,1,0,'2019-01-25 12:41:46',11,'2019-01-25 12:41:50',11);
/*!40000 ALTER TABLE `caption` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `caption_language`
--

DROP TABLE IF EXISTS `caption_language`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `caption_language` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `language_id` int(11) NOT NULL,
  `caption_id` int(11) NOT NULL,
  `caption` varchar(4000) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_caption_language_language_idx` (`language_id`),
  CONSTRAINT `pk_caption_language_language` FOREIGN KEY (`language_id`) REFERENCES `language` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `caption_language`
--

LOCK TABLES `caption_language` WRITE;
/*!40000 ALTER TABLE `caption_language` DISABLE KEYS */;
INSERT INTO `caption_language` VALUES (4,1,1,'3523fr'),(5,2,1,'3473473'),(6,1,2,'Mật khẩu không đúng'),(7,2,2,'Password is wrong'),(8,1,16,'Thịt'),(9,2,16,'Meat'),(10,1,17,'124124'),(11,2,17,'4124124'),(12,1,18,'23t23'),(13,2,18,'3t23t'),(14,1,19,'sdas'),(15,2,19,'asdasd'),(16,1,20,'23r2'),(17,2,20,'23r23r23r'),(18,1,21,'t34'),(19,2,21,'4t34t'),(20,1,22,'23f2'),(21,2,22,'23f23f32'),(22,1,23,'Cá'),(23,2,23,'Fish'),(24,1,24,'Thịt'),(25,2,24,'Meat'),(26,1,25,'Trái cây và rau củ'),(27,2,25,'Fruit & Vegetable'),(28,1,26,'Nhà sản xuất hoặc nhà cung ứng'),(29,2,26,'The manufacturer or supplier'),(30,1,27,'Nhà sản xuất hoặc nhà cung ứng'),(31,2,27,'The manufacturer or supplier'),(32,1,28,'Nhà nhập khẩu hoặc nhà bán buôn'),(33,2,28,'The importer or wholesaler'),(34,1,29,'Nhà bán lẻ'),(35,2,29,'The retailer'),(36,1,30,'Khác'),(37,2,30,'Other'),(38,1,31,'Tiêu chuẩn VietGAP'),(39,2,31,'VietGAP Standard');
/*!40000 ALTER TABLE `caption_language` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `caption_name_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (1,'M','Meat',24,1,0,'2019-01-15 04:44:54',11,'2019-01-15 10:51:48',11),(2,'F','Fish',23,1,0,'2019-01-15 04:45:30',11,'2019-01-15 10:52:08',11),(3,'FV','Fruit & Vegetable',25,1,0,'2019-01-15 04:45:49',11,'2019-01-15 10:52:44',11);
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `company`
--

DROP TABLE IF EXISTS `company`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `company` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `tax_code` varchar(45) COLLATE utf8_unicode_ci DEFAULT '',
  `website` varchar(45) COLLATE utf8_unicode_ci DEFAULT '',
  `contact_id` int(11) DEFAULT NULL,
  `address_id` int(11) DEFAULT NULL,
  `logo_path` varchar(200) COLLATE utf8_unicode_ci DEFAULT '',
  `description` varchar(2000) COLLATE utf8_unicode_ci DEFAULT '',
  `company_type_id` int(11) DEFAULT NULL,
  `gs1_code` int(11) NOT NULL DEFAULT '0',
  `is_partner` tinyint(4) NOT NULL DEFAULT '0',
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL DEFAULT '0',
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `company`
--

LOCK TABLES `company` WRITE;
/*!40000 ALTER TABLE `company` DISABLE KEYS */;
INSERT INTO `company` VALUES (15,'CÔNG TY CỔ PHẦN FPT','0101248141','fpt.com.vn',21,22,'15/logo/logo_636829193261594669.jpeg','',2,8325,0,1,0,'2019-01-12 12:18:19',11,'2019-01-21 04:14:22',11),(16,'CÔNG TY CỔ PHẦN FPT','0101248141','fpt.com.vn',22,23,'16/logo/logo_636829194402710417.jpeg','',3,0,0,1,0,'2019-01-12 12:50:40',11,'2019-01-18 08:17:37',11);
/*!40000 ALTER TABLE `company` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `company_type`
--

DROP TABLE IF EXISTS `company_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `company_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `caption_name_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `company_type`
--

LOCK TABLES `company_type` WRITE;
/*!40000 ALTER TABLE `company_type` DISABLE KEYS */;
INSERT INTO `company_type` VALUES (1,'MS','The manufacturer or supplier',26,1,0,'2019-01-16 03:34:30',11,'2019-01-16 03:34:30',11),(2,'IW','The importer or wholesaler',28,1,0,'2019-01-16 03:39:16',11,'2019-01-16 03:40:02',11),(3,'RE','The retailer',29,1,0,'2019-01-16 03:40:55',11,'2019-01-16 04:17:40',11),(4,'OT','Other',30,1,0,'2019-01-16 04:17:15',11,'2019-01-16 04:17:34',11);
/*!40000 ALTER TABLE `company_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contact`
--

DROP TABLE IF EXISTS `contact`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contact` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `object_type` varchar(10) CHARACTER SET latin1 DEFAULT NULL,
  `object_id` int(11) DEFAULT NULL COMMENT 'The object id is a id of object reference',
  `name` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `phone` varchar(15) CHARACTER SET latin1 DEFAULT NULL,
  `email` varchar(100) CHARACTER SET latin1 DEFAULT NULL,
  `gender` varchar(10) CHARACTER SET latin1 NOT NULL DEFAULT '',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contact`
--

LOCK TABLES `contact` WRITE;
/*!40000 ALTER TABLE `contact` DISABLE KEYS */;
INSERT INTO `contact` VALUES (1,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-04 06:02:21',11,'2019-01-04 06:02:21',11),(2,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-04 06:08:38',11,'2019-01-04 06:08:38',11),(3,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-04 06:09:59',11,'2019-01-04 06:09:59',11),(4,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-04 06:10:26',11,'2019-01-04 06:10:26',11),(5,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-04 06:43:57',11,'2019-01-04 06:43:57',11),(6,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-04 06:51:53',11,'2019-01-04 06:51:53',11),(7,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-04 06:55:50',11,'2019-01-04 06:55:50',11),(8,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-11 11:27:58',11,'2019-01-11 11:27:58',11),(9,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-11 11:28:34',11,'2019-01-11 11:28:34',11),(10,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-11 11:38:23',11,'2019-01-11 11:38:23',11),(11,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-11 11:42:16',11,'2019-01-11 11:42:16',11),(12,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-11 11:47:07',11,'2019-01-11 11:47:07',11),(13,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-11 11:48:05',11,'2019-01-11 11:48:05',11),(14,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-11 11:49:16',11,'2019-01-11 11:49:16',11),(15,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-11 11:54:45',11,'2019-01-11 11:54:45',11),(16,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-11 13:00:12',11,'2019-01-11 13:00:12',11),(17,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-12 12:04:50',11,'2019-01-12 12:04:50',11),(18,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-12 12:04:57',11,'2019-01-12 12:04:57',11),(19,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-12 12:13:00',11,'2019-01-12 12:13:00',11),(20,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-12 12:13:07',11,'2019-01-12 12:13:07',11),(21,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-12 12:18:19',11,'2019-01-12 12:18:19',11),(22,'company',NULL,'Nguyễn Văn A','0968888888','vannguyen@fpt.com.vn','male',0,'2019-01-12 12:50:40',11,'2019-01-12 12:50:40',11);
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
  `code` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `phone_code` varchar(10) CHARACTER SET latin1 DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  `gs1_code` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `country`
--

LOCK TABLES `country` WRITE;
/*!40000 ALTER TABLE `country` DISABLE KEYS */;
INSERT INTO `country` VALUES (1,'vn-vi','Việt Nam','084',1,0,'2019-01-02 06:33:59',11,'2019-01-11 06:40:27',11,NULL),(2,'us-en','US','010',1,0,'2019-01-11 06:34:32',11,'2019-01-11 07:24:26',11,NULL);
/*!40000 ALTER TABLE `country` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `district`
--

DROP TABLE IF EXISTS `district`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `district` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET latin1 DEFAULT NULL,
  `name` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `country_id` int(11) DEFAULT NULL,
  `province_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_district_country_idx` (`country_id`),
  KEY `pk_district_province_idx` (`province_id`),
  CONSTRAINT `pk_district_country` FOREIGN KEY (`country_id`) REFERENCES `country` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_district_province` FOREIGN KEY (`province_id`) REFERENCES `province` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `district`
--

LOCK TABLES `district` WRITE;
/*!40000 ALTER TABLE `district` DISABLE KEYS */;
INSERT INTO `district` VALUES (1,'Q1','Q.1',1,1,1,0,'2019-01-02 06:33:59',0,'2019-01-07 09:53:54',11),(2,'Q2','Q.2',1,1,1,0,'2019-01-02 06:33:59',0,'2019-01-07 09:53:58',11),(3,'Q3','Q.3',1,1,1,0,'2019-01-02 06:33:59',0,'2019-01-07 09:54:07',11),(4,'BH','Biên Hòa',1,2,1,0,'2019-01-02 06:33:59',0,'2019-01-07 09:54:16',11),(5,'XL','Xuân Lộc',1,2,1,0,'2019-01-02 06:33:59',0,'2019-01-07 09:54:21',11),(6,'DQ','Định Quán',1,2,1,0,'2019-01-02 06:33:59',0,'2019-01-07 09:54:28',11),(7,'Q4','Q.4',1,1,1,0,'2019-01-07 09:53:33',11,'2019-01-07 09:53:47',11);
/*!40000 ALTER TABLE `district` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `farmer`
--

DROP TABLE IF EXISTS `farmer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `farmer` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `contact_id` int(11) DEFAULT NULL,
  `address_id` int(11) DEFAULT NULL,
  `farmer_type` int(11) NOT NULL DEFAULT '1',
  `description` varchar(500) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `farmer`
--

LOCK TABLES `farmer` WRITE;
/*!40000 ALTER TABLE `farmer` DISABLE KEYS */;
/*!40000 ALTER TABLE `farmer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `farmer_type`
--

DROP TABLE IF EXISTS `farmer_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `farmer_type` (
  `id` int(11) NOT NULL,
  `name` varchar(45) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `farmer_type`
--

LOCK TABLES `farmer_type` WRITE;
/*!40000 ALTER TABLE `farmer_type` DISABLE KEYS */;
INSERT INTO `farmer_type` VALUES (1,'household'),(2,'cooperative');
/*!40000 ALTER TABLE `farmer_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `gln`
--

DROP TABLE IF EXISTS `gln`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `gln` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `company_id` int(11) NOT NULL,
  `type` varchar(45) DEFAULT NULL,
  `used_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_public` tinyint(4) NOT NULL DEFAULT '1',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  `is_deleted` tinyint(4) DEFAULT NULL,
  `country_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `gln`
--

LOCK TABLES `gln` WRITE;
/*!40000 ALTER TABLE `gln` DISABLE KEYS */;
INSERT INTO `gln` VALUES (1,15,NULL,'2019-01-29 17:00:00',1,1,'2019-01-29 17:00:00',1,'2019-01-29 17:00:00',2,NULL,NULL);
/*!40000 ALTER TABLE `gln` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `growing_method`
--

DROP TABLE IF EXISTS `growing_method`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `growing_method` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `caption_name_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `growing_method`
--

LOCK TABLES `growing_method` WRITE;
/*!40000 ALTER TABLE `growing_method` DISABLE KEYS */;
INSERT INTO `growing_method` VALUES (1,'VietGAP','VietGAP Standard',31,1,0,'2019-01-25 12:41:46',11,'2019-01-25 12:41:50',11);
/*!40000 ALTER TABLE `growing_method` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `gtin`
--

DROP TABLE IF EXISTS `gtin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `gtin` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `indicator_digit` tinyint(4) NOT NULL DEFAULT '0',
  `company_code` int(11) NOT NULL DEFAULT '0',
  `numeric` bigint(20) NOT NULL DEFAULT '0',
  `check_digit` tinyint(4) DEFAULT NULL,
  `company_id` int(11) NOT NULL,
  `type` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `used_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `gtin`
--

LOCK TABLES `gtin` WRITE;
/*!40000 ALTER TABLE `gtin` DISABLE KEYS */;
INSERT INTO `gtin` VALUES (1,0,8325,2354455,2,15,'gtin_12','2019-01-22 09:01:08',0,'2019-01-22 09:01:08',11,'2019-01-22 09:01:08',11),(2,0,8325,2,6,15,'gtin_8','2019-01-22 09:14:59',1,'2019-01-22 09:14:59',11,'2019-01-26 11:38:50',11),(3,0,8325,33323,0,15,'gtin_8','2019-01-22 09:43:43',0,'2019-01-22 09:43:43',11,'2019-01-26 11:39:11',11);
/*!40000 ALTER TABLE `gtin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `language`
--

DROP TABLE IF EXISTS `language`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `language` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `code` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `description` varchar(256) CHARACTER SET utf8 DEFAULT NULL,
  `class_icon` varchar(100) DEFAULT NULL,
  `country_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `language`
--

LOCK TABLES `language` WRITE;
/*!40000 ALTER TABLE `language` DISABLE KEYS */;
INSERT INTO `language` VALUES (1,'Tiếng Việt','vi','Việt Nam','flag-icon flag-icon-vn',1,1,0,'2019-01-09 17:00:00',0,'2019-01-14 11:47:29',11),(2,'English','en','English','flag-icon flag-icon-us',2,1,0,'2019-01-09 17:00:00',0,'2019-01-14 11:47:35',11);
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
  `code` varchar(10) CHARACTER SET latin1 DEFAULT NULL,
  `image_path` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `default_name` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `default_decription` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `category_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,'CRT','1/default/prod_636831496429852699.jpeg','carrot','Description product',3,1,0,'2019-01-15 04:47:22',11,'2019-01-15 11:17:36',11);
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
  `name` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `description` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_product_language_language_idx` (`language_id`),
  KEY `pk_product_language_product_idx` (`product_id`),
  CONSTRAINT `pk_product_language_language` FOREIGN KEY (`language_id`) REFERENCES `language` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_product_language_product` FOREIGN KEY (`product_id`) REFERENCES `product` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_language`
--

LOCK TABLES `product_language` WRITE;
/*!40000 ALTER TABLE `product_language` DISABLE KEYS */;
INSERT INTO `product_language` VALUES (1,1,1,'Cà rốt','Mô tả'),(2,1,2,'carrot','Description ');
/*!40000 ALTER TABLE `product_language` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `production`
--

DROP TABLE IF EXISTS `production`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `production` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `company_id` int(11) NOT NULL,
  `product_id` int(11) DEFAULT NULL,
  `gtin_id` int(11) NOT NULL,
  `growing_method_id` int(11) DEFAULT NULL,
  `country_of_origin` varchar(100) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `trademark` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `commercial_claim` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `product_size` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `grade` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `colour` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `shape` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `variety` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `commercial_type` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `colour_of_flesh` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `post_harvest_treatment` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `post_harvest_processing` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `cooking_type` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `seed_properties` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `trade_package_content_quantity` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `trade_unit_package_type` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `consumer_unit_content_quantity` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `trade_unit` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `comsumer_unit_package_type` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `comsumer_unit` varchar(100) COLLATE utf8_unicode_ci DEFAULT '',
  `production_image_id` int(11) DEFAULT NULL,
  `is_public` tinyint(4) NOT NULL DEFAULT '1',
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `production`
--

LOCK TABLES `production` WRITE;
/*!40000 ALTER TABLE `production` DISABLE KEYS */;
INSERT INTO `production` VALUES (1,'6469846',15,1,2,1,'VietNam','Coopmart',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,1,1,0,'2019-01-17 03:59:15',11,'2019-01-26 11:38:50',11),(2,'165416',15,1,3,1,'Vietnam','Big C',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,1,1,0,'2019-01-17 04:00:05',11,'2019-01-26 11:39:11',11);
/*!40000 ALTER TABLE `production` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `production_image`
--

DROP TABLE IF EXISTS `production_image`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `production_image` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `front` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `left` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `top` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `back` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `right` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `bottom` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `production_image`
--

LOCK TABLES `production_image` WRITE;
/*!40000 ALTER TABLE `production_image` DISABLE KEYS */;
/*!40000 ALTER TABLE `production_image` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `production_process`
--

DROP TABLE IF EXISTS `production_process`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `production_process` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `company_id` int(11) NOT NULL,
  `code` varchar(45) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `point_id` int(11) DEFAULT NULL,
  `production_id` int(11) DEFAULT NULL,
  `product_id` int(11) DEFAULT NULL,
  `farmer_id` int(11) DEFAULT NULL,
  `company_cultivation_id` int(11) DEFAULT NULL,
  `company_collection_id` int(11) DEFAULT NULL,
  `company_fulfillment_id` int(11) DEFAULT NULL,
  `company_distribution_id` int(11) DEFAULT NULL,
  `company_retailer_id` int(11) DEFAULT NULL,
  `collection_date` timestamp NULL DEFAULT NULL,
  `fulfillment_date` timestamp NULL DEFAULT NULL,
  `distribution_date` timestamp NULL DEFAULT NULL,
  `retailer_date` timestamp NULL DEFAULT NULL,
  `expiry_date` timestamp NULL DEFAULT NULL,
  `manufacturing_date` timestamp NULL DEFAULT NULL,
  `growing_method_id` int(11) DEFAULT NULL,
  `standard_expiry_date` timestamp NULL DEFAULT NULL,
  `description` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `quantity` decimal(10,0) DEFAULT '0',
  `uom` varchar(20) COLLATE utf8_unicode_ci DEFAULT '',
  `is_new` tinyint(4) NOT NULL DEFAULT '1',
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `production_process`
--

LOCK TABLES `production_process` WRITE;
/*!40000 ALTER TABLE `production_process` DISABLE KEYS */;
INSERT INTO `production_process` VALUES (1,15,'0000000051',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,0,NULL,0,1,1,'2019-01-24 10:47:36',11,'2019-01-24 11:08:01',11),(2,15,'0000000052',NULL,2,1,NULL,15,16,15,15,16,NULL,NULL,NULL,NULL,NULL,NULL,1,NULL,NULL,0,NULL,0,1,0,'2019-01-24 10:58:55',11,'2019-01-26 11:01:53',11),(4,15,'0000000054',NULL,1,1,NULL,15,15,15,16,16,NULL,NULL,NULL,NULL,NULL,NULL,1,NULL,NULL,0,NULL,0,1,0,'2019-01-24 11:08:34',11,'2019-01-26 11:26:35',11);
/*!40000 ALTER TABLE `production_process` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `province`
--

DROP TABLE IF EXISTS `province`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `province` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET latin1 DEFAULT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `phone_code` varchar(10) CHARACTER SET latin1 DEFAULT NULL,
  `country_id` int(11) DEFAULT NULL,
  `region_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_province_country_idx` (`country_id`),
  KEY `pk_province_region_idx` (`region_id`),
  CONSTRAINT `pk_province_country` FOREIGN KEY (`country_id`) REFERENCES `country` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_province_region` FOREIGN KEY (`region_id`) REFERENCES `region` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `province`
--

LOCK TABLES `province` WRITE;
/*!40000 ALTER TABLE `province` DISABLE KEYS */;
INSERT INTO `province` VALUES (1,'HCM','Hồ Chí Minh','028',1,NULL,1,0,'2019-01-03 08:57:44',0,'2019-01-07 07:18:34',11),(2,'DNai','Đồng Nai','0251',1,1,1,0,'2019-01-03 08:58:49',0,'2019-01-07 07:40:22',11),(3,'LD','Lâm Đồng','0263',1,NULL,1,0,'2019-01-07 07:18:10',11,'2019-01-07 07:18:10',11);
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
  `code` varchar(10) CHARACTER SET utf8 DEFAULT NULL,
  `name` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `country_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_region_country_idx` (`country_id`),
  CONSTRAINT `pk_region_country` FOREIGN KEY (`country_id`) REFERENCES `country` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `region`
--

LOCK TABLES `region` WRITE;
/*!40000 ALTER TABLE `region` DISABLE KEYS */;
INSERT INTO `region` VALUES (1,'DNB','Đông Nam Bộ',1,1,0,'2019-01-07 07:31:41',11,'2019-01-07 07:31:41',11),(2,'TNB','Tây Nam Bộ',1,1,0,'2019-01-07 07:32:45',11,'2019-01-07 07:32:45',11);
/*!40000 ALTER TABLE `region` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) CHARACTER SET utf8 NOT NULL,
  `description` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `is_external_role` tinyint(4) NOT NULL DEFAULT '0',
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Standard','Standard',0,1,0,'2019-01-10 06:31:48',11,'2019-01-10 07:57:36',11),(2,'Administrator','Administrator',0,1,0,'2019-01-09 17:00:00',0,'2019-01-09 17:00:00',0),(3,'Partner','Partner Administrator',0,1,0,'2019-01-10 07:55:12',11,'2019-01-10 07:55:12',11),(4,'Guest','Guest',0,1,0,'2019-01-10 07:56:30',11,'2019-01-10 07:56:30',11);
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schedule_action`
--

DROP TABLE IF EXISTS `schedule_action`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `schedule_action` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `action_type` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `data` varchar(20000) CHARACTER SET utf8 NOT NULL DEFAULT '{}',
  `action_result` varchar(20) COLLATE utf8_unicode_ci NOT NULL DEFAULT '0',
  `down_count` int(11) NOT NULL DEFAULT '0',
  `message` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schedule_action`
--

LOCK TABLES `schedule_action` WRITE;
/*!40000 ALTER TABLE `schedule_action` DISABLE KEYS */;
INSERT INTO `schedule_action` VALUES (2,'deletefile','{\"path\":\"System.Threading.Tasks.Task`1[System.String]/\"}','notstart',3,NULL,'2019-01-12 12:18:27',11);
/*!40000 ALTER TABLE `schedule_action` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `session_buffer`
--

DROP TABLE IF EXISTS `session_buffer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `session_buffer` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `session_id` bigint(11) NOT NULL,
  `company_id` int(11) NOT NULL,
  `data_json` varchar(20000) COLLATE utf8_unicode_ci NOT NULL DEFAULT '{}',
  `type` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `expired_date` timestamp NULL DEFAULT NULL,
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=247 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `session_buffer`
--

LOCK TABLES `session_buffer` WRITE;
/*!40000 ALTER TABLE `session_buffer` DISABLE KEYS */;
INSERT INTO `session_buffer` VALUES (147,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":4,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:52.2588235+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:52.2588258+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:52.2588257+07:00\"}','GTIN','2019-01-29 09:33:52',0,'2019-01-29 07:33:52',11,'2019-01-29 07:33:52',11),(148,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":5,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:53.6121286+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:53.61213+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:53.6121299+07:00\"}','GTIN','2019-01-29 09:33:53',0,'2019-01-29 07:33:53',11,'2019-01-29 07:33:53',11),(149,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":6,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:54.2531377+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:54.2531381+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:54.253138+07:00\"}','GTIN','2019-01-29 09:33:54',0,'2019-01-29 07:33:54',11,'2019-01-29 07:33:54',11),(150,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":7,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:54.7251689+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:54.7251693+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:54.7251692+07:00\"}','GTIN','2019-01-29 09:33:54',0,'2019-01-29 07:33:54',11,'2019-01-29 07:33:54',11),(151,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":8,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:54.9337036+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:54.9337041+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:54.933704+07:00\"}','GTIN','2019-01-29 09:33:54',0,'2019-01-29 07:33:54',11,'2019-01-29 07:33:54',11),(152,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":9,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:55.1377542+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:55.1377547+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:55.1377546+07:00\"}','GTIN','2019-01-29 09:33:55',0,'2019-01-29 07:33:55',11,'2019-01-29 07:33:55',11),(153,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":10,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:55.335654+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:55.3356544+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:55.3356543+07:00\"}','GTIN','2019-01-29 09:33:55',0,'2019-01-29 07:33:55',11,'2019-01-29 07:33:55',11),(154,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":11,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:55.5352251+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:55.5352255+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:55.5352255+07:00\"}','GTIN','2019-01-29 09:33:55',0,'2019-01-29 07:33:55',11,'2019-01-29 07:33:55',11),(155,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":12,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:55.7240677+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:55.7240683+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:55.7240682+07:00\"}','GTIN','2019-01-29 09:33:55',0,'2019-01-29 07:33:55',11,'2019-01-29 07:33:55',11),(156,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":13,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:55.9144647+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:55.9144658+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:55.9144658+07:00\"}','GTIN','2019-01-29 09:33:55',0,'2019-01-29 07:33:55',11,'2019-01-29 07:33:55',11),(157,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":14,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:56.1052005+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:56.1052009+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:56.1052009+07:00\"}','GTIN','2019-01-29 09:33:56',0,'2019-01-29 07:33:56',11,'2019-01-29 07:33:56',11),(158,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":15,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:56.2949703+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:56.2949708+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:56.2949707+07:00\"}','GTIN','2019-01-29 09:33:56',0,'2019-01-29 07:33:56',11,'2019-01-29 07:33:56',11),(159,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":16,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:56.4892596+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:56.48926+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:56.48926+07:00\"}','GTIN','2019-01-29 09:33:56',0,'2019-01-29 07:33:56',11,'2019-01-29 07:33:56',11),(160,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":17,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:56.6776176+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:56.677618+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:56.6776179+07:00\"}','GTIN','2019-01-29 09:33:56',0,'2019-01-29 07:33:56',11,'2019-01-29 07:33:56',11),(161,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":18,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:56.8778671+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:56.8778675+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:56.8778675+07:00\"}','GTIN','2019-01-29 09:33:56',0,'2019-01-29 07:33:56',11,'2019-01-29 07:33:56',11),(162,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":19,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:57.0644704+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:57.064471+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:57.0644709+07:00\"}','GTIN','2019-01-29 09:33:57',0,'2019-01-29 07:33:57',11,'2019-01-29 07:33:57',11),(163,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":20,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:57.2538782+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:57.2538789+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:57.2538788+07:00\"}','GTIN','2019-01-29 09:33:57',0,'2019-01-29 07:33:57',11,'2019-01-29 07:33:57',11),(164,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":21,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:57.4415739+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:57.4415743+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:57.4415742+07:00\"}','GTIN','2019-01-29 09:33:57',0,'2019-01-29 07:33:57',11,'2019-01-29 07:33:57',11),(165,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":22,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:57.6425855+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:57.6425863+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:57.6425863+07:00\"}','GTIN','2019-01-29 09:33:57',0,'2019-01-29 07:33:57',11,'2019-01-29 07:33:57',11),(166,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":23,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:57.8355558+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:57.8355562+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:57.8355561+07:00\"}','GTIN','2019-01-29 09:33:57',0,'2019-01-29 07:33:57',11,'2019-01-29 07:33:57',11),(167,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":24,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:58.0433222+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:58.0433227+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:58.0433226+07:00\"}','GTIN','2019-01-29 09:33:58',0,'2019-01-29 07:33:58',11,'2019-01-29 07:33:58',11),(168,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":25,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:58.2254772+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:58.2254776+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:58.2254776+07:00\"}','GTIN','2019-01-29 09:33:58',0,'2019-01-29 07:33:58',11,'2019-01-29 07:33:58',11),(169,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":26,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:58.4448517+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:58.4448521+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:58.444852+07:00\"}','GTIN','2019-01-29 09:33:58',0,'2019-01-29 07:33:58',11,'2019-01-29 07:33:58',11),(170,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":27,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:58.6381359+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:58.6381373+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:58.6381372+07:00\"}','GTIN','2019-01-29 09:33:58',0,'2019-01-29 07:33:58',11,'2019-01-29 07:33:58',11),(171,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":28,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:59.0390496+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:59.0390501+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:59.03905+07:00\"}','GTIN','2019-01-29 09:33:59',0,'2019-01-29 07:33:59',11,'2019-01-29 07:33:59',11),(172,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":29,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:59.244319+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:59.2443195+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:59.2443194+07:00\"}','GTIN','2019-01-29 09:33:59',0,'2019-01-29 07:33:59',11,'2019-01-29 07:33:59',11),(173,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":30,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:59.441282+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:59.4412829+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:59.4412828+07:00\"}','GTIN','2019-01-29 09:33:59',0,'2019-01-29 07:33:59',11,'2019-01-29 07:33:59',11),(174,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":31,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:59.6471584+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:59.6471588+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:59.6471587+07:00\"}','GTIN','2019-01-29 09:33:59',0,'2019-01-29 07:33:59',11,'2019-01-29 07:33:59',11),(175,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":32,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:33:59.8568434+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:33:59.8568438+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:33:59.8568438+07:00\"}','GTIN','2019-01-29 09:33:59',0,'2019-01-29 07:33:59',11,'2019-01-29 07:33:59',11),(176,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":33,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:00.0478386+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:00.0478391+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:00.047839+07:00\"}','GTIN','2019-01-29 09:34:00',0,'2019-01-29 07:34:00',11,'2019-01-29 07:34:00',11),(177,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":34,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:00.2407009+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:00.2407014+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:00.2407013+07:00\"}','GTIN','2019-01-29 09:34:00',0,'2019-01-29 07:34:00',11,'2019-01-29 07:34:00',11),(178,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":35,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:00.4274675+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:00.4274683+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:00.4274683+07:00\"}','GTIN','2019-01-29 09:34:00',0,'2019-01-29 07:34:00',11,'2019-01-29 07:34:00',11),(179,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":36,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:00.6088534+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:00.608854+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:00.608854+07:00\"}','GTIN','2019-01-29 09:34:00',0,'2019-01-29 07:34:00',11,'2019-01-29 07:34:00',11),(180,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":37,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:00.8227362+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:00.8227367+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:00.8227366+07:00\"}','GTIN','2019-01-29 09:34:00',0,'2019-01-29 07:34:00',11,'2019-01-29 07:34:00',11),(181,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":38,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:01.0183604+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:01.0183608+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:01.0183607+07:00\"}','GTIN','2019-01-29 09:34:01',0,'2019-01-29 07:34:01',11,'2019-01-29 07:34:01',11),(182,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":39,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:01.2045002+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:01.2045008+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:01.2045008+07:00\"}','GTIN','2019-01-29 09:34:01',0,'2019-01-29 07:34:01',11,'2019-01-29 07:34:01',11),(183,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":40,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:01.3978096+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:01.3978101+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:01.39781+07:00\"}','GTIN','2019-01-29 09:34:01',0,'2019-01-29 07:34:01',11,'2019-01-29 07:34:01',11),(184,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":41,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:01.6117007+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:01.6117011+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:01.611701+07:00\"}','GTIN','2019-01-29 09:34:01',0,'2019-01-29 07:34:01',11,'2019-01-29 07:34:01',11),(185,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":42,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:01.8171497+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:01.8171501+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:01.81715+07:00\"}','GTIN','2019-01-29 09:34:01',0,'2019-01-29 07:34:01',11,'2019-01-29 07:34:01',11),(186,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":43,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:02.0144373+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:02.0144379+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:02.0144378+07:00\"}','GTIN','2019-01-29 09:34:02',0,'2019-01-29 07:34:02',11,'2019-01-29 07:34:02',11),(187,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":44,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:02.207561+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:02.2075616+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:02.2075615+07:00\"}','GTIN','2019-01-29 09:34:02',0,'2019-01-29 07:34:02',11,'2019-01-29 07:34:02',11),(188,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":45,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:02.4220807+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:02.4220829+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:02.4220828+07:00\"}','GTIN','2019-01-29 09:34:02',0,'2019-01-29 07:34:02',11,'2019-01-29 07:34:02',11),(189,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":46,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:02.6256101+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:02.6256287+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:02.6256286+07:00\"}','GTIN','2019-01-29 09:34:02',0,'2019-01-29 07:34:02',11,'2019-01-29 07:34:02',11),(190,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":47,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:02.8136992+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:02.8136996+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:02.8136995+07:00\"}','GTIN','2019-01-29 09:34:02',0,'2019-01-29 07:34:02',11,'2019-01-29 07:34:02',11),(191,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":48,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:03.0282938+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:03.0282946+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:03.0282945+07:00\"}','GTIN','2019-01-29 09:34:03',0,'2019-01-29 07:34:03',11,'2019-01-29 07:34:03',11),(192,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":49,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:03.2143818+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:03.2143823+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:03.2143822+07:00\"}','GTIN','2019-01-29 09:34:03',0,'2019-01-29 07:34:03',11,'2019-01-29 07:34:03',11),(193,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":50,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:03.4161021+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:03.4161033+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:03.4161032+07:00\"}','GTIN','2019-01-29 09:34:03',0,'2019-01-29 07:34:03',11,'2019-01-29 07:34:03',11),(194,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":51,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:03.6195821+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:03.6195826+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:03.6195825+07:00\"}','GTIN','2019-01-29 09:34:03',0,'2019-01-29 07:34:03',11,'2019-01-29 07:34:03',11),(195,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":52,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:03.8070022+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:03.8070029+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:03.8070028+07:00\"}','GTIN','2019-01-29 09:34:03',0,'2019-01-29 07:34:03',11,'2019-01-29 07:34:03',11),(196,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":53,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:04.0182102+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:04.0182106+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:04.0182106+07:00\"}','GTIN','2019-01-29 09:34:04',0,'2019-01-29 07:34:04',11,'2019-01-29 07:34:04',11),(197,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":54,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:04.2061873+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:04.2061877+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:04.2061877+07:00\"}','GTIN','2019-01-29 09:34:04',0,'2019-01-29 07:34:04',11,'2019-01-29 07:34:04',11),(198,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":55,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:04.4270557+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:04.4270565+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:04.4270564+07:00\"}','GTIN','2019-01-29 09:34:04',0,'2019-01-29 07:34:04',11,'2019-01-29 07:34:04',11),(199,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":56,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:04.6340251+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:04.6340255+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:04.6340254+07:00\"}','GTIN','2019-01-29 09:34:04',0,'2019-01-29 07:34:04',11,'2019-01-29 07:34:04',11),(200,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":57,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:04.8502558+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:04.8502562+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:04.8502561+07:00\"}','GTIN','2019-01-29 09:34:04',0,'2019-01-29 07:34:04',11,'2019-01-29 07:34:04',11),(201,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":58,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:05.0554774+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:05.0554781+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:05.055478+07:00\"}','GTIN','2019-01-29 09:34:05',0,'2019-01-29 07:34:05',11,'2019-01-29 07:34:05',11),(202,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":59,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:05.2736988+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:05.2736993+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:05.2736992+07:00\"}','GTIN','2019-01-29 09:34:05',0,'2019-01-29 07:34:05',11,'2019-01-29 07:34:05',11),(203,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":60,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:05.4696086+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:05.469609+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:05.469609+07:00\"}','GTIN','2019-01-29 09:34:05',0,'2019-01-29 07:34:05',11,'2019-01-29 07:34:05',11),(204,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":61,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:05.662287+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:05.6622875+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:05.6622874+07:00\"}','GTIN','2019-01-29 09:34:05',0,'2019-01-29 07:34:05',11,'2019-01-29 07:34:05',11),(205,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":62,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:05.8679264+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:05.8679268+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:05.8679268+07:00\"}','GTIN','2019-01-29 09:34:05',0,'2019-01-29 07:34:05',11,'2019-01-29 07:34:05',11),(206,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":63,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:06.0736285+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:06.0736289+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:06.0736289+07:00\"}','GTIN','2019-01-29 09:34:06',0,'2019-01-29 07:34:06',11,'2019-01-29 07:34:06',11),(207,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":64,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:06.2805575+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:06.280558+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:06.2805579+07:00\"}','GTIN','2019-01-29 09:34:06',0,'2019-01-29 07:34:06',11,'2019-01-29 07:34:06',11),(208,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":65,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:06.4909273+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:06.4909278+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:06.4909277+07:00\"}','GTIN','2019-01-29 09:34:06',0,'2019-01-29 07:34:06',11,'2019-01-29 07:34:06',11),(209,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":66,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:06.6887947+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:06.6887952+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:06.6887951+07:00\"}','GTIN','2019-01-29 09:34:06',0,'2019-01-29 07:34:06',11,'2019-01-29 07:34:06',11),(210,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":67,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:06.883796+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:06.8837966+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:06.8837965+07:00\"}','GTIN','2019-01-29 09:34:06',0,'2019-01-29 07:34:06',11,'2019-01-29 07:34:06',11),(211,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":68,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:07.0872006+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:07.087201+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:07.087201+07:00\"}','GTIN','2019-01-29 09:34:07',0,'2019-01-29 07:34:07',11,'2019-01-29 07:34:07',11),(212,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":69,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:07.283456+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:07.2834597+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:07.2834596+07:00\"}','GTIN','2019-01-29 09:34:07',0,'2019-01-29 07:34:07',11,'2019-01-29 07:34:07',11),(213,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":70,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:07.4991458+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:07.4991463+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:07.4991462+07:00\"}','GTIN','2019-01-29 09:34:07',0,'2019-01-29 07:34:07',11,'2019-01-29 07:34:07',11),(214,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":71,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:07.679469+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:07.6794698+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:07.6794697+07:00\"}','GTIN','2019-01-29 09:34:07',0,'2019-01-29 07:34:07',11,'2019-01-29 07:34:07',11),(215,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":72,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:07.8900595+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:07.8900599+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:07.8900599+07:00\"}','GTIN','2019-01-29 09:34:07',0,'2019-01-29 07:34:07',11,'2019-01-29 07:34:07',11),(216,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":73,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:08.0931919+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:08.0931923+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:08.0931923+07:00\"}','GTIN','2019-01-29 09:34:08',0,'2019-01-29 07:34:08',11,'2019-01-29 07:34:08',11),(217,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":74,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:08.3170352+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:08.3170357+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:08.3170356+07:00\"}','GTIN','2019-01-29 09:34:08',0,'2019-01-29 07:34:08',11,'2019-01-29 07:34:08',11),(218,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":75,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:08.6087011+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:08.6087015+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:08.6087014+07:00\"}','GTIN','2019-01-29 09:34:08',0,'2019-01-29 07:34:08',11,'2019-01-29 07:34:08',11),(219,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":76,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:14.3619992+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:14.3619998+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:14.3619997+07:00\"}','GTIN','2019-01-29 09:34:14',0,'2019-01-29 07:34:14',11,'2019-01-29 07:34:14',11),(220,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":77,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:15.1011582+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:15.1011613+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:15.1011612+07:00\"}','GTIN','2019-01-29 09:34:15',0,'2019-01-29 07:34:15',11,'2019-01-29 07:34:15',11),(221,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":78,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:15.3064741+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:15.3064746+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:15.3064745+07:00\"}','GTIN','2019-01-29 09:34:15',0,'2019-01-29 07:34:15',11,'2019-01-29 07:34:15',11),(222,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":79,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:15.7054946+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:15.7054951+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:15.705495+07:00\"}','GTIN','2019-01-29 09:34:15',0,'2019-01-29 07:34:15',11,'2019-01-29 07:34:15',11),(223,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":80,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:15.9017937+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:15.9017942+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:15.9017942+07:00\"}','GTIN','2019-01-29 09:34:15',0,'2019-01-29 07:34:15',11,'2019-01-29 07:34:15',11),(224,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":81,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:16.1039034+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:16.1039048+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:16.1039047+07:00\"}','GTIN','2019-01-29 09:34:16',0,'2019-01-29 07:34:16',11,'2019-01-29 07:34:16',11),(225,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":82,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:16.3096507+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:16.3096511+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:16.309651+07:00\"}','GTIN','2019-01-29 09:34:16',0,'2019-01-29 07:34:16',11,'2019-01-29 07:34:16',11),(226,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":83,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:16.5349581+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:16.5349612+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:16.5349611+07:00\"}','GTIN','2019-01-29 09:34:16',0,'2019-01-29 07:34:16',11,'2019-01-29 07:34:16',11),(227,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":84,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:16.7339607+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:16.7339617+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:16.7339616+07:00\"}','GTIN','2019-01-29 09:34:16',0,'2019-01-29 07:34:16',11,'2019-01-29 07:34:16',11),(228,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":85,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:16.9278611+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:16.9278615+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:16.9278615+07:00\"}','GTIN','2019-01-29 09:34:16',0,'2019-01-29 07:34:16',11,'2019-01-29 07:34:16',11),(229,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":86,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:17.1401864+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:17.140187+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:17.140187+07:00\"}','GTIN','2019-01-29 09:34:17',0,'2019-01-29 07:34:17',11,'2019-01-29 07:34:17',11),(230,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":87,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:17.3345665+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:17.334567+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:17.3345669+07:00\"}','GTIN','2019-01-29 09:34:17',0,'2019-01-29 07:34:17',11,'2019-01-29 07:34:17',11),(231,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":88,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:17.5393514+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:17.539352+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:17.5393519+07:00\"}','GTIN','2019-01-29 09:34:17',0,'2019-01-29 07:34:17',11,'2019-01-29 07:34:17',11),(232,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":89,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:17.7383329+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:17.7383336+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:17.7383335+07:00\"}','GTIN','2019-01-29 09:34:17',0,'2019-01-29 07:34:17',11,'2019-01-29 07:34:17',11),(233,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":90,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:17.9308667+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:17.9308698+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:17.9308697+07:00\"}','GTIN','2019-01-29 09:34:17',0,'2019-01-29 07:34:17',11,'2019-01-29 07:34:17',11),(234,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":91,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:18.3544939+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:18.3544947+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:18.3544946+07:00\"}','GTIN','2019-01-29 09:34:18',0,'2019-01-29 07:34:18',11,'2019-01-29 07:34:18',11),(235,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":92,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:21.274796+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:21.2747967+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:21.2747967+07:00\"}','GTIN','2019-01-29 09:34:21',0,'2019-01-29 07:34:21',11,'2019-01-29 07:34:21',11),(236,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":93,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:21.4999977+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:21.4999981+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:21.4999981+07:00\"}','GTIN','2019-01-29 09:34:21',0,'2019-01-29 07:34:21',11,'2019-01-29 07:34:21',11),(237,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":94,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:21.7101697+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:21.7101702+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:21.7101702+07:00\"}','GTIN','2019-01-29 09:34:21',0,'2019-01-29 07:34:21',11,'2019-01-29 07:34:21',11),(238,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":95,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:21.8960034+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:21.896004+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:21.896004+07:00\"}','GTIN','2019-01-29 09:34:21',0,'2019-01-29 07:34:21',11,'2019-01-29 07:34:21',11),(239,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":96,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:22.0865687+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:22.0865695+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:22.0865694+07:00\"}','GTIN','2019-01-29 09:34:22',0,'2019-01-29 07:34:22',11,'2019-01-29 07:34:22',11),(240,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":97,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:22.286478+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:22.2864784+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:22.2864784+07:00\"}','GTIN','2019-01-29 09:34:22',0,'2019-01-29 07:34:22',11,'2019-01-29 07:34:22',11),(241,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":98,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:22.4792749+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:22.4792757+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:22.4792756+07:00\"}','GTIN','2019-01-29 09:34:22',0,'2019-01-29 07:34:22',11,'2019-01-29 07:34:22',11),(242,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":99,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:22.6826991+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:22.6827005+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:22.6827004+07:00\"}','GTIN','2019-01-29 09:34:22',0,'2019-01-29 07:34:22',11,'2019-01-29 07:34:22',11),(243,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":100,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:22.8831348+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:22.8831354+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:22.8831353+07:00\"}','GTIN','2019-01-29 09:34:22',0,'2019-01-29 07:34:22',11,'2019-01-29 07:34:22',11),(244,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":101,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:23.0986341+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:23.0986347+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:23.0986346+07:00\"}','GTIN','2019-01-29 09:34:23',0,'2019-01-29 07:34:23',11,'2019-01-29 07:34:23',11),(245,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":102,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:23.2817195+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:23.2817202+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:23.2817201+07:00\"}','GTIN','2019-01-29 09:34:23',0,'2019-01-29 07:34:23',11,'2019-01-29 07:34:23',11),(246,178,15,'{\"Id\":0,\"IndicatorDigit\":0,\"CompanyCode\":8325,\"Numeric\":103,\"CheckDigit\":0,\"CompanyId\":15,\"Type\":0,\"UserDate\":\"2019-01-29T14:34:23.5036209+07:00\",\"IsUsed\":true,\"CreatedBy\":11,\"CreatedDate\":\"2019-01-29T14:34:23.5036215+07:00\",\"ModifiedBy\":11,\"ModifiedDate\":\"2019-01-29T14:34:23.5036214+07:00\"}','GTIN','2019-01-29 09:34:23',0,'2019-01-29 07:34:23',11,'2019-01-29 07:34:23',11);
/*!40000 ALTER TABLE `session_buffer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `setting`
--

DROP TABLE IF EXISTS `setting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `setting` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `value` varchar(2000) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `description` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `setting`
--

LOCK TABLES `setting` WRITE;
/*!40000 ALTER TABLE `setting` DISABLE KEYS */;
INSERT INTO `setting` VALUES (2,'LangDefault','en',NULL,'2019-01-10 10:13:38',11,'2019-01-10 10:13:38',11),(3,'Path.Company','E:/Projects/aritnt/SRC/front-end/src/assets/aritrace/companies',NULL,'2019-01-12 10:54:12',11,'2019-01-15 04:49:42',11),(4,'Path.Product','E:/Projects/aritnt/SRC/front-end/src/assets/aritrace/products',NULL,'2019-01-15 04:49:25',11,'2019-01-15 04:49:25',11),(6,'Role.Default','1','','2019-01-28 12:32:49',0,'2019-01-28 12:32:49',0);
/*!40000 ALTER TABLE `setting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `step`
--

DROP TABLE IF EXISTS `step`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `step` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `production_id` int(11) DEFAULT NULL,
  `code` varchar(45) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `step_type_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `step`
--

LOCK TABLES `step` WRITE;
/*!40000 ALTER TABLE `step` DISABLE KEYS */;
/*!40000 ALTER TABLE `step` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `step_type`
--

DROP TABLE IF EXISTS `step_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `step_type` (
  `id` int(11) NOT NULL,
  `name` varchar(45) COLLATE utf8_unicode_ci DEFAULT '',
  `description` varchar(45) COLLATE utf8_unicode_ci DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `step_type`
--

LOCK TABLES `step_type` WRITE;
/*!40000 ALTER TABLE `step_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `step_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `storage`
--

DROP TABLE IF EXISTS `storage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `storage` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `key` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `value` bigint(20) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `storage`
--

LOCK TABLES `storage` WRITE;
/*!40000 ALTER TABLE `storage` DISABLE KEYS */;
INSERT INTO `storage` VALUES (1,'tracecode',58);
/*!40000 ALTER TABLE `storage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uom`
--

DROP TABLE IF EXISTS `uom`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `uom` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) NOT NULL,
  `is_used` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uom`
--

LOCK TABLES `uom` WRITE;
/*!40000 ALTER TABLE `uom` DISABLE KEYS */;
INSERT INTO `uom` VALUES (1,'kg','');
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
) ENGINE=InnoDB AUTO_INCREMENT=190 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_access_token`
--

LOCK TABLES `user_access_token` WRITE;
/*!40000 ALTER TABLE `user_access_token` DISABLE KEYS */;
INSERT INTO `user_access_token` VALUES (184,11,'v/8L7W6/hlAkX8r36xdkRhcjYp/p5CupWSQxJB96IC606BnkdQshAk3STcnBeAJN','2019-01-30 10:51:25','9999-12-31 23:59:59'),(186,11,'v/8L7W6/hlAkX8r36xdkRuYok+FFpv40/XuFsPnE6Nq06BnkdQshAk3STcnBeAJN','2019-01-30 14:47:53','9999-12-31 23:59:59'),(189,11,'v/8L7W6/hlAkX8r36xdkRi/oU5mzuc7Gsil39ct6cWO06BnkdQshAk3STcnBeAJN','2019-01-31 10:56:39','9999-12-31 23:59:59');
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
  `user_name` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `security_password` char(36) CHARACTER SET latin1 NOT NULL,
  `password_reset_code` char(8) CHARACTER SET latin1 DEFAULT NULL,
  `is_external_user` tinyint(4) DEFAULT '1',
  `is_superadmin` tinyint(4) NOT NULL DEFAULT '0',
  `company_id` int(11) DEFAULT NULL,
  `is_deleted` tinyint(4) DEFAULT '0',
  `is_actived` tinyint(4) NOT NULL DEFAULT '0',
  `is_used` tinyint(4) DEFAULT '1',
  `created_by` int(11) DEFAULT NULL,
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_date` timestamp NULL DEFAULT NULL,
  `modified_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_account`
--

LOCK TABLES `user_account` WRITE;
/*!40000 ALTER TABLE `user_account` DISABLE KEYS */;
INSERT INTO `user_account` VALUES (10,'hoangnguyen','D7023BAD32181EF8A7FF7FA171399D70','string','019e5ce5-2a8c-48ea-a5f7-160626d0fa44',NULL,0,0,NULL,1,1,1,0,'2018-12-17 20:45:08','2019-01-10 07:57:52',11),(11,'admin','D7023BAD32181EF8A7FF7FA171399D70','admin@gmail.com','019e5ce5-2a8c-48ea-a5f7-160626d0fa44',NULL,0,0,15,0,1,1,0,'2018-12-18 14:15:22','2019-01-18 10:56:06',11),(16,'haula','517A81A408BC741830713A0A5BA1FA80','lahau@gmail.com','09a223b1-bc21-42fe-b807-dcd81dccb9e1',NULL,0,0,NULL,1,0,1,0,'2018-12-18 14:33:51','2019-01-10 07:57:48',11),(17,'supervisorKhoi','B023B537E8AC3BF091A1472AC733D6DE','vankhoi@gmail.com','7de1f578-9c06-42ff-8c71-27126679bb69',NULL,0,0,NULL,1,1,1,11,'2018-12-18 14:34:51','2019-01-10 07:57:50',11),(18,'khoinguyen','927568FF9A9F250F9DC9C497701D90D6','khoinguyen@gmail.com','2ed9a699-3213-408b-9865-72351b19ad9b',NULL,0,0,15,0,1,1,11,'2019-01-10 09:29:32','2019-01-18 07:22:10',11),(19,'thienvo','277B72F5764E96C83A434D69A4FA54C0','thienvo@gmail.com','c59f841d-5134-469b-b4a3-c28676fab99b',NULL,0,0,15,0,1,1,11,'2019-01-10 09:30:48','2019-01-18 07:22:15',11),(20,'hoangnguyen','E045100BB9088A2E75F6B313ECCD7C2E','hoangnguyen@gmail.com','d6cee737-71ed-40e1-9ff8-636ba1553a99',NULL,0,0,15,0,1,1,11,'2019-01-10 09:37:25','2019-01-18 07:22:21',11),(21,'retailer002','43211ABC71CCB9D4C914CA25C19DD1DD','retailer001@gmail.com','b51adc12-fc46-4f37-baf2-093ced28c291',NULL,0,0,15,0,1,1,11,'2019-01-18 08:07:29','2019-01-28 12:28:56',11),(22,'retailer003','17A1698F088DB8717C0FFAAC0B7C3CDA','retailer003@gmail.com','8b37353d-0a46-47c0-807e-9cd5bed4cf15',NULL,0,0,16,0,1,1,11,'2019-01-28 12:40:43','2019-01-28 12:40:43',11);
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
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_account_role`
--

LOCK TABLES `user_account_role` WRITE;
/*!40000 ALTER TABLE `user_account_role` DISABLE KEYS */;
INSERT INTO `user_account_role` VALUES (10,11,2,'2019-01-10 05:00:00'),(11,10,1,'2019-01-10 07:48:47'),(12,16,1,'2019-01-10 07:49:05'),(13,17,1,'2019-01-10 07:50:03'),(14,18,3,'2019-01-10 09:31:21'),(15,20,3,'2019-01-10 09:37:25'),(16,19,3,'2019-01-10 09:37:34'),(17,21,3,'2019-01-18 08:07:29'),(18,22,1,'2019-01-28 12:40:43');
/*!40000 ALTER TABLE `user_account_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ward`
--

DROP TABLE IF EXISTS `ward`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ward` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(10) CHARACTER SET latin1 DEFAULT NULL,
  `name` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `country_id` int(11) DEFAULT NULL,
  `province_id` int(11) DEFAULT NULL,
  `district_id` int(11) DEFAULT NULL,
  `is_used` tinyint(4) NOT NULL DEFAULT '1',
  `is_deleted` tinyint(4) NOT NULL DEFAULT '0',
  `created_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_by` int(11) NOT NULL,
  `modified_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `modified_by` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pk_ward_country_idx` (`country_id`),
  KEY `pk_ward_province_idx` (`province_id`),
  KEY `pk_ward_district_idx` (`district_id`),
  CONSTRAINT `pk_ward_country` FOREIGN KEY (`country_id`) REFERENCES `country` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_ward_district` FOREIGN KEY (`district_id`) REFERENCES `district` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pk_ward_province` FOREIGN KEY (`province_id`) REFERENCES `province` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ward`
--

LOCK TABLES `ward` WRITE;
/*!40000 ALTER TABLE `ward` DISABLE KEYS */;
INSERT INTO `ward` VALUES (1,'P1','Phường 1',1,1,1,1,0,'2019-01-02 06:33:59',0,'2019-01-07 10:34:17',11),(2,'P2','Phường 2',1,1,1,1,0,'2019-01-02 06:33:59',0,'2019-01-07 10:34:22',11),(3,'P3','Phường 3',1,1,1,1,0,'2019-01-02 06:33:59',0,'2019-01-07 10:34:26',11),(4,'P1','Phường 1',1,1,2,1,0,'2019-01-02 06:33:59',0,'2019-01-07 10:34:32',11),(5,'P2','Phường 2',1,1,2,1,0,'2019-01-02 06:33:59',0,'2019-01-07 10:34:38',11),(6,'P5','Phường 5',1,1,2,1,0,'2019-01-02 06:33:59',0,'2019-01-07 10:34:45',11);
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

-- Dump completed on 2019-01-31 16:33:19
