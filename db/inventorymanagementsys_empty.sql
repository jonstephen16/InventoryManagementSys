-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 17, 2024 at 12:41 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `inventorymanagementsys`
--

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `CategoryID` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`CategoryID`, `Name`, `Status`) VALUES
(2, 'Atomizers', 1),
(3, 'Battery', 1),
(4, 'Charger', 1),
(5, 'Cotton', 1),
(6, 'Devices', 1),
(7, 'Disposables', 1),
(8, 'Flavor', 1),
(9, 'Juiceline', 1),
(10, 'MSC', 1),
(11, 'OCC', 1),
(12, 'Pods', 1),
(13, 'RELX Juices', 1),
(14, 'Wire', 1);

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `ProductID` int(11) NOT NULL,
  `Sku` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Description` text DEFAULT NULL,
  `Category` varchar(255) NOT NULL,
  `SellingPrice` decimal(20,2) NOT NULL,
  `Unit` varchar(255) NOT NULL,
  `Status` int(11) NOT NULL DEFAULT 1,
  `CreatedBy` int(11) NOT NULL DEFAULT 1,
  `DateCreated` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` int(11) DEFAULT NULL,
  `DateUpdated` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`ProductID`, `Sku`, `Name`, `Description`, `Category`, `SellingPrice`, `Unit`, `Status`, `CreatedBy`, `DateCreated`, `UpdatedBy`, `DateUpdated`) VALUES
(1, 'PROD-2024-0001', 'BLACK ELITE 8000 - Battery', '', 'Battery', 250.00, 'pcs', 1, 1, '2024-11-16 01:35:35', NULL, NULL),
(2, 'PROD-2024-0002', 'BLACK ELITE 8000 - Mixed Berries', '', 'Juiceline', 300.00, 'pcs', 1, 1, '2024-11-16 01:36:17', NULL, NULL),
(3, 'PROD-2024-0003', 'BLACK ELITE 8000 - Yakult', '', 'Juiceline', 300.00, 'pcs', 1, 1, '2024-11-16 01:36:48', 1, '2024-11-16 01:37:31'),
(4, 'PROD-2024-0004', 'BLACK LITE 8000 - Taro Ice', '', 'Juiceline', 300.00, 'pcs', 1, 1, '2024-11-16 01:38:19', NULL, NULL),
(5, 'PROD-2024-0005', 'BLACK ELITE 8000 - Watermelon', '', 'Juiceline', 300.00, 'pcs', 1, 1, '2024-11-16 03:33:44', NULL, NULL),
(6, 'PROD-2024-0006', 'BLACK ELITE 8000 - Black Currant', '', 'Juiceline', 300.00, 'pcs', 1, 1, '2024-11-16 03:42:44', NULL, NULL),
(7, 'PROD-2024-0007', 'BLACK ELITE 8000 - Strawberry', '', 'Juiceline', 300.00, 'pcs', 1, 1, '2024-11-16 16:47:18', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `purchases`
--

CREATE TABLE `purchases` (
  `PurchaseID` int(11) NOT NULL,
  `ReferenceNo` varchar(255) NOT NULL,
  `Amount` decimal(20,2) NOT NULL,
  `Description` text DEFAULT NULL,
  `Status` int(11) NOT NULL DEFAULT 4,
  `CreatedBy` int(11) NOT NULL,
  `DateCreated` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` int(11) DEFAULT NULL,
  `DateUpdated` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `purchase_products`
--

CREATE TABLE `purchase_products` (
  `ID` int(11) NOT NULL,
  `PurchaseID` int(11) NOT NULL,
  `ProductID` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `UnitPrice` decimal(20,2) NOT NULL,
  `TotalAmount` decimal(20,2) NOT NULL,
  `Status` int(11) NOT NULL DEFAULT 1,
  `CreatedBy` int(11) NOT NULL,
  `DateCreated` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` int(11) DEFAULT NULL,
  `DateUpdated` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `sales`
--

CREATE TABLE `sales` (
  `SalesID` int(11) NOT NULL,
  `ReferenceNo` varchar(255) NOT NULL,
  `Amount` decimal(20,2) NOT NULL,
  `Description` text NOT NULL,
  `Status` int(11) NOT NULL DEFAULT 4,
  `CreatedBy` int(11) NOT NULL DEFAULT 1,
  `DateCreated` int(11) NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` int(11) DEFAULT NULL,
  `DateUpdated` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `sales_products`
--

CREATE TABLE `sales_products` (
  `ID` int(11) NOT NULL,
  `SalesID` int(11) NOT NULL,
  `ProductID` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `UnitPrice` decimal(20,2) NOT NULL,
  `TotalAmount` decimal(20,2) NOT NULL,
  `CreatedBy` int(11) NOT NULL DEFAULT 1,
  `DateCreated` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` int(11) DEFAULT NULL,
  `DateUpdated` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `status`
--

CREATE TABLE `status` (
  `StatusID` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `status`
--

INSERT INTO `status` (`StatusID`, `Name`) VALUES
(1, 'Active'),
(6, 'Cancelled'),
(7, 'Completed'),
(3, 'Deleted'),
(2, 'Inactive'),
(4, 'Pending'),
(5, 'Received');

-- --------------------------------------------------------

--
-- Table structure for table `stocks`
--

CREATE TABLE `stocks` (
  `StockID` int(11) NOT NULL,
  `ProductID` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `DateCreated` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` int(11) DEFAULT NULL,
  `DateUpdated` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `units`
--

CREATE TABLE `units` (
  `UnitID` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `units`
--

INSERT INTO `units` (`UnitID`, `Name`, `Status`) VALUES
(1, 'pcs', 1);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserID` int(11) NOT NULL,
  `UserRoleID` int(11) NOT NULL,
  `Username` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Firstname` varchar(255) NOT NULL,
  `Lastname` varchar(255) NOT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Address` varchar(500) DEFAULT NULL,
  `Status` int(11) NOT NULL DEFAULT 1,
  `CreatedBy` int(11) NOT NULL DEFAULT 1,
  `DateCreated` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` int(11) NOT NULL DEFAULT 1,
  `DateUpdated` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserID`, `UserRoleID`, `Username`, `Password`, `Firstname`, `Lastname`, `Email`, `Address`, `Status`, `CreatedBy`, `DateCreated`, `UpdatedBy`, `DateUpdated`) VALUES
(1, 1, 'admin', '5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8', 'System', 'Admin', 'system.admin@inventorysys.com', 'Legazpi City', 1, 1, '2024-11-14 00:06:06', 1, '2024-11-15 00:46:30'),
(2, 3, 'user', '5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8', 'System', 'User', 'system.user@inventorysys.com', 'Legazpi City', 1, 1, '2024-11-14 00:06:06', 1, '2024-11-15 00:46:57');

-- --------------------------------------------------------

--
-- Table structure for table `user_roles`
--

CREATE TABLE `user_roles` (
  `UserRoleID` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Status` int(11) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user_roles`
--

INSERT INTO `user_roles` (`UserRoleID`, `Name`, `Status`) VALUES
(1, 'Administrator', 1),
(2, 'Manager', 1),
(3, 'Staff', 1),
(4, 'Owner', 1);

-- --------------------------------------------------------

--
-- Table structure for table `user_session_logs`
--

CREATE TABLE `user_session_logs` (
  `UserSessionLogID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `LoggedIn` datetime NOT NULL,
  `LoggedOut` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`CategoryID`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`ProductID`),
  ADD UNIQUE KEY `UniqueSkuNameProducts` (`Sku`,`Name`) USING BTREE;

--
-- Indexes for table `purchases`
--
ALTER TABLE `purchases`
  ADD PRIMARY KEY (`PurchaseID`),
  ADD UNIQUE KEY `UniqueReferenceNoSales` (`ReferenceNo`) USING BTREE;

--
-- Indexes for table `purchase_products`
--
ALTER TABLE `purchase_products`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `sales`
--
ALTER TABLE `sales`
  ADD PRIMARY KEY (`SalesID`),
  ADD UNIQUE KEY `UniqueReferenceNoSales` (`ReferenceNo`);

--
-- Indexes for table `sales_products`
--
ALTER TABLE `sales_products`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `status`
--
ALTER TABLE `status`
  ADD PRIMARY KEY (`StatusID`),
  ADD UNIQUE KEY `Name` (`Name`);

--
-- Indexes for table `stocks`
--
ALTER TABLE `stocks`
  ADD PRIMARY KEY (`StockID`),
  ADD UNIQUE KEY `UniqueProductIDStocks` (`ProductID`) USING BTREE;

--
-- Indexes for table `units`
--
ALTER TABLE `units`
  ADD PRIMARY KEY (`UnitID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserID`);

--
-- Indexes for table `user_roles`
--
ALTER TABLE `user_roles`
  ADD PRIMARY KEY (`UserRoleID`);

--
-- Indexes for table `user_session_logs`
--
ALTER TABLE `user_session_logs`
  ADD PRIMARY KEY (`UserSessionLogID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `CategoryID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `ProductID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `purchases`
--
ALTER TABLE `purchases`
  MODIFY `PurchaseID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `purchase_products`
--
ALTER TABLE `purchase_products`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `sales`
--
ALTER TABLE `sales`
  MODIFY `SalesID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `sales_products`
--
ALTER TABLE `sales_products`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `status`
--
ALTER TABLE `status`
  MODIFY `StatusID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `stocks`
--
ALTER TABLE `stocks`
  MODIFY `StockID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `units`
--
ALTER TABLE `units`
  MODIFY `UnitID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `user_roles`
--
ALTER TABLE `user_roles`
  MODIFY `UserRoleID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `user_session_logs`
--
ALTER TABLE `user_session_logs`
  MODIFY `UserSessionLogID` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
