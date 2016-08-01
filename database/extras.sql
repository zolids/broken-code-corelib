/*
Navicat SQL Server Data Transfer

Source Server         : SQL_LOCAL
Source Server Version : 120000
Source Host           : ITJCAPC:1433
Source Database       : fsxcv
Source Schema         : extras

Target Server Type    : SQL Server
Target Server Version : 120000
File Encoding         : 65001

Date: 2016-08-01 17:17:06
*/


-- ----------------------------
-- Table structure for tbl_custom_fields
-- ----------------------------
DROP TABLE [extras].[tbl_custom_fields]
GO
CREATE TABLE [extras].[tbl_custom_fields] (
[id] int NOT NULL IDENTITY(1,1) ,
[label_name] varchar(250) NULL ,
[field_value] text NULL ,
[module] varchar(250) NULL ,
[sort_index] int NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of tbl_custom_fields
-- ----------------------------
SET IDENTITY_INSERT [extras].[tbl_custom_fields] ON
GO
SET IDENTITY_INSERT [extras].[tbl_custom_fields] OFF
GO

-- ----------------------------
-- Table structure for tbl_emails
-- ----------------------------
DROP TABLE [extras].[tbl_emails]
GO
CREATE TABLE [extras].[tbl_emails] (
[reference_code] varchar(100) NULL ,
[body] text NULL ,
[datetime] datetime NULL ,
[retry] int NULL DEFAULT ((0)) ,
[sent] int NULL DEFAULT ((0)) ,
[id] int NOT NULL IDENTITY(1,1) ,
[reference_table] varchar(20) NULL ,
[subject] varchar(250) NULL ,
[include_this_email] varchar(250) NULL ,
[modified_date] datetime NULL 
)


GO
DBCC CHECKIDENT(N'[extras].[tbl_emails]', RESEED, 22)
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'extras', 
'TABLE', N'tbl_emails', 
'COLUMN', N'retry')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'at least 3 times'
, @level0type = 'SCHEMA', @level0name = N'extras'
, @level1type = 'TABLE', @level1name = N'tbl_emails'
, @level2type = 'COLUMN', @level2name = N'retry'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'at least 3 times'
, @level0type = 'SCHEMA', @level0name = N'extras'
, @level1type = 'TABLE', @level1name = N'tbl_emails'
, @level2type = 'COLUMN', @level2name = N'retry'
GO

-- ----------------------------
-- Records of tbl_emails
-- ----------------------------
SET IDENTITY_INSERT [extras].[tbl_emails] ON
GO
INSERT INTO [extras].[tbl_emails] ([reference_code], [body], [datetime], [retry], [sent], [id], [reference_table], [subject], [include_this_email], [modified_date]) VALUES (N'27', N'<div style=''background:#f4f4f4;margin:40px 0;padding:40px 0'' bgcolor=''#f4f4f4''><div class=''adM''></div><table width=''550'' border=''0'' cellspacing=''0'' cellpadding=''0'' style=''background:#fff;border:1px solid #ccc;border-radius:5px'' bgcolor=''#FFFFFF'' align=''center''><tbody><tr><td style=''padding:20px;font-family:Arial,Helvetica,sans-serif;font-size:12px''><span class=''im''><h3 style=''font-family:Arial,Helvetica,sans-serif;font-size:19px;font-weight:normal;margin-bottom:20px;margin-top:0;color:#333333''><font face=''Arial, Helvetica, sans-serif'' color=''#333333''>Vechile Requisition Request</font></h3><p>A request has been received to reset the password for the following account:</p></span><p><span class=''im''><strong>Username</strong>: jennocabrito<br></span></p><p>To continue, please visit the following link:</p><p><a href=''http://localhost:37407/Acquisition/createEditAcquisition?isNew=false&request_id=27'' target=''_blank''>http://localhost:37407/Acquisition/createEditAcquisition?isNew=false&request_id=27</a></p><span class=''im''><p>The request is valid only for 24 hours.</p><p>If you did not make this request, simply ignore this email.</p></span><p></p></td></tr><tr><td style=''padding:20px;border-top:1px dotted #ccc''><a href=''localhost'' target=''_blank'' data-saferedirecturl=''''><img src='''' alt='''' style=''display:block;margin:0;border:none'' class=''CToWUd''></a></td></tr></tbody></table><div class=''yj6qo''></div><div class=''adL''></div></div>', N'2016-07-25 17:35:09.987', N'0', N'0', N'19', N'tbl_request', N'Vehicle Requesition Form', N'jcabrito@a-m-s.ae', null)
GO
GO
INSERT INTO [extras].[tbl_emails] ([reference_code], [body], [datetime], [retry], [sent], [id], [reference_table], [subject], [include_this_email], [modified_date]) VALUES (N'28', N'<div style=''background:#f4f4f4;margin:40px 0;padding:40px 0'' bgcolor=''#f4f4f4''><div class=''adM''></div><table width=''550'' border=''0'' cellspacing=''0'' cellpadding=''0'' style=''background:#fff;border:1px solid #ccc;border-radius:5px'' bgcolor=''#FFFFFF'' align=''center''><tbody><tr><td style=''padding:20px;font-family:Arial,Helvetica,sans-serif;font-size:12px''><span class=''im''><h3 style=''font-family:Arial,Helvetica,sans-serif;font-size:19px;font-weight:normal;margin-bottom:20px;margin-top:0;color:#333333''><font face=''Arial, Helvetica, sans-serif'' color=''#333333''>Vechile Requisition Request</font></h3><p>A request has been received to reset the password for the following account:</p></span><p><span class=''im''><strong>Username</strong>: jennocabrito<br></span></p><p>To continue, please visit the following link:</p><p><a href=''http://localhost:37407/Acquisition/createEditAcquisition?isNew=false&request_id=28'' target=''_blank''>http://localhost:37407/Acquisition/createEditAcquisition?isNew=false&request_id=28</a></p><span class=''im''><p>The request is valid only for 24 hours.</p><p>If you did not make this request, simply ignore this email.</p></span><p></p></td></tr><tr><td style=''padding:20px;border-top:1px dotted #ccc''><a href=''localhost'' target=''_blank'' data-saferedirecturl=''''><img src='''' alt='''' style=''display:block;margin:0;border:none'' class=''CToWUd''></a></td></tr></tbody></table><div class=''yj6qo''></div><div class=''adL''></div></div>', N'2016-07-26 14:34:31.507', N'0', N'0', N'20', N'tbl_request', N'Vehicle Requesition Form', N'jcabrito@a-m-s.ae', null)
GO
GO
INSERT INTO [extras].[tbl_emails] ([reference_code], [body], [datetime], [retry], [sent], [id], [reference_table], [subject], [include_this_email], [modified_date]) VALUES (N'29', N'<div style=''background:#f4f4f4;margin:40px 0;padding:40px 0'' bgcolor=''#f4f4f4''><div class=''adM''></div><table width=''550'' border=''0'' cellspacing=''0'' cellpadding=''0'' style=''background:#fff;border:1px solid #ccc;border-radius:5px'' bgcolor=''#FFFFFF'' align=''center''><tbody><tr><td style=''padding:20px;font-family:Arial,Helvetica,sans-serif;font-size:12px''><span class=''im''><h3 style=''font-family:Arial,Helvetica,sans-serif;font-size:19px;font-weight:normal;margin-bottom:20px;margin-top:0;color:#333333''><font face=''Arial, Helvetica, sans-serif'' color=''#333333''>Vechile Requisition Request</font></h3><p>A request has been received to reset the password for the following account:</p></span><p><span class=''im''><strong>Username</strong>: jennocabrito<br></span></p><p>To continue, please visit the following link:</p><p><a href=''http://localhost:37407/Acquisition/createEditAcquisition?isNew=false&request_id=29'' target=''_blank''>http://localhost:37407/Acquisition/createEditAcquisition?isNew=false&request_id=29</a></p><span class=''im''><p>The request is valid only for 24 hours.</p><p>If you did not make this request, simply ignore this email.</p></span><p></p></td></tr><tr><td style=''padding:20px;border-top:1px dotted #ccc''><a href=''localhost'' target=''_blank'' data-saferedirecturl=''''><img src='''' alt='''' style=''display:block;margin:0;border:none'' class=''CToWUd''></a></td></tr></tbody></table><div class=''yj6qo''></div><div class=''adL''></div></div>', N'2016-07-26 14:39:23.217', N'0', N'0', N'21', N'tbl_request', N'Vehicle Requesition Form', N'jcabrito@a-m-s.ae', null)
GO
GO
INSERT INTO [extras].[tbl_emails] ([reference_code], [body], [datetime], [retry], [sent], [id], [reference_table], [subject], [include_this_email], [modified_date]) VALUES (N'30', N'<div style=''background:#f4f4f4;margin:40px 0;padding:40px 0'' bgcolor=''#f4f4f4''><div class=''adM''></div><table width=''550'' border=''0'' cellspacing=''0'' cellpadding=''0'' style=''background:#fff;border:1px solid #ccc;border-radius:5px'' bgcolor=''#FFFFFF'' align=''center''><tbody><tr><td style=''padding:20px;font-family:Arial,Helvetica,sans-serif;font-size:12px''><span class=''im''><h3 style=''font-family:Arial,Helvetica,sans-serif;font-size:19px;font-weight:normal;margin-bottom:20px;margin-top:0;color:#333333''><font face=''Arial, Helvetica, sans-serif'' color=''#333333''>Vechile Requisition Request</font></h3><p>A request has been received to reset the password for the following account:</p></span><p><span class=''im''><strong>Username</strong>: jennocabrito<br></span></p><p>To continue, please visit the following link:</p><p><a href=''http://localhost:37407/Acquisition/createEditAcquisition?isNew=false&request_id=30'' target=''_blank''>http://localhost:37407/Acquisition/createEditAcquisition?isNew=false&request_id=30</a></p><span class=''im''><p>The request is valid only for 24 hours.</p><p>If you did not make this request, simply ignore this email.</p></span><p></p></td></tr><tr><td style=''padding:20px;border-top:1px dotted #ccc''><a href=''localhost'' target=''_blank'' data-saferedirecturl=''''><img src='''' alt='''' style=''display:block;margin:0;border:none'' class=''CToWUd''></a></td></tr></tbody></table><div class=''yj6qo''></div><div class=''adL''></div></div>', N'2016-07-26 14:41:16.207', N'0', N'0', N'22', N'tbl_request', N'Vehicle Requesition Form', N'jcabrito@a-m-s.ae', null)
GO
GO
SET IDENTITY_INSERT [extras].[tbl_emails] OFF
GO

-- ----------------------------
-- Table structure for tbl_logs
-- ----------------------------
DROP TABLE [extras].[tbl_logs]
GO
CREATE TABLE [extras].[tbl_logs] (
[id] int NOT NULL IDENTITY(1,1) ,
[type] varchar(20) NULL ,
[source] varchar(50) NULL ,
[event_type] varchar(50) NULL ,
[message] text NULL ,
[reference_code] varchar(100) NULL ,
[datetime] datetime NULL ,
[user_id] int NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'extras', 
'TABLE', N'tbl_logs', 
'COLUMN', N'type')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'type can be ''logs'' or ''email'''
, @level0type = 'SCHEMA', @level0name = N'extras'
, @level1type = 'TABLE', @level1name = N'tbl_logs'
, @level2type = 'COLUMN', @level2name = N'type'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'type can be ''logs'' or ''email'''
, @level0type = 'SCHEMA', @level0name = N'extras'
, @level1type = 'TABLE', @level1name = N'tbl_logs'
, @level2type = 'COLUMN', @level2name = N'type'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'extras', 
'TABLE', N'tbl_logs', 
'COLUMN', N'source')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'source of event'
, @level0type = 'SCHEMA', @level0name = N'extras'
, @level1type = 'TABLE', @level1name = N'tbl_logs'
, @level2type = 'COLUMN', @level2name = N'source'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'source of event'
, @level0type = 'SCHEMA', @level0name = N'extras'
, @level1type = 'TABLE', @level1name = N'tbl_logs'
, @level2type = 'COLUMN', @level2name = N'source'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'extras', 
'TABLE', N'tbl_logs', 
'COLUMN', N'event_type')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'INSERT, UPDATE, DELETE'
, @level0type = 'SCHEMA', @level0name = N'extras'
, @level1type = 'TABLE', @level1name = N'tbl_logs'
, @level2type = 'COLUMN', @level2name = N'event_type'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'INSERT, UPDATE, DELETE'
, @level0type = 'SCHEMA', @level0name = N'extras'
, @level1type = 'TABLE', @level1name = N'tbl_logs'
, @level2type = 'COLUMN', @level2name = N'event_type'
GO

-- ----------------------------
-- Records of tbl_logs
-- ----------------------------
SET IDENTITY_INSERT [extras].[tbl_logs] ON
GO
SET IDENTITY_INSERT [extras].[tbl_logs] OFF
GO

-- ----------------------------
-- Table structure for tbl_options
-- ----------------------------
DROP TABLE [extras].[tbl_options]
GO
CREATE TABLE [extras].[tbl_options] (
[id] int NOT NULL IDENTITY(1,1) ,
[name] varchar(50) NULL ,
[value] text NULL 
)


GO
DBCC CHECKIDENT(N'[extras].[tbl_options]', RESEED, 19)
GO

-- ----------------------------
-- Records of tbl_options
-- ----------------------------
SET IDENTITY_INSERT [extras].[tbl_options] ON
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'1', N'timezone', N'Asia/Dubai')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'2', N'timeformat', N'd/m/Y')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'3', N'allowed_file_types', N'pdf,.xlt,.xls,.xml,.xml,docx,.docm,.dotx,.dotm,.docb,jpeg,png,gif')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'4', N'admin_email_address', N'info@ams.com')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'5', N'mail_system_use', N'smtp')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'6', N'mail_smtp_host', N'smtp.gmail.com')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'7', N'mail_smtp_port', N'587')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'8', N'mail_smtp_user', N'fmsfze238@gmail.com')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'9', N'mail_smtp_pass', N'fm$123456')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'10', N'mail_from_name', N'Online Fastrax')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'11', N'mail_smtp_auth', N'none')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'12', N'allow_sending_email', N'no')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'13', N'allow_editing_picklist', N'no')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'14', N'file_types_limit_to', N'all')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'15', N'pass_require_upper', N'0')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'16', N'pass_require_lower', N'0')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'17', N'pass_require_number', N'0')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'18', N'pass_require_special', N'0')
GO
GO
INSERT INTO [extras].[tbl_options] ([id], [name], [value]) VALUES (N'19', N'bbc_email_recipient', null)
GO
GO
SET IDENTITY_INSERT [extras].[tbl_options] OFF
GO

-- ----------------------------
-- Table structure for tbl_project_sites
-- ----------------------------
DROP TABLE [extras].[tbl_project_sites]
GO
CREATE TABLE [extras].[tbl_project_sites] (
[id] int NOT NULL IDENTITY(1,1) ,
[name] varchar(250) NULL ,
[project_id] int NULL 
)


GO
DBCC CHECKIDENT(N'[extras].[tbl_project_sites]', RESEED, 2)
GO

-- ----------------------------
-- Records of tbl_project_sites
-- ----------------------------
SET IDENTITY_INSERT [extras].[tbl_project_sites] ON
GO
INSERT INTO [extras].[tbl_project_sites] ([id], [name], [project_id]) VALUES (N'1', N'DHO', N'1')
GO
GO
INSERT INTO [extras].[tbl_project_sites] ([id], [name], [project_id]) VALUES (N'2', N'WORKSHOP', N'1')
GO
GO
SET IDENTITY_INSERT [extras].[tbl_project_sites] OFF
GO

-- ----------------------------
-- Table structure for tbl_projects
-- ----------------------------
DROP TABLE [extras].[tbl_projects]
GO
CREATE TABLE [extras].[tbl_projects] (
[id] int NOT NULL IDENTITY(1,1) ,
[name] varchar(250) NULL 
)


GO
DBCC CHECKIDENT(N'[extras].[tbl_projects]', RESEED, 7)
GO

-- ----------------------------
-- Records of tbl_projects
-- ----------------------------
SET IDENTITY_INSERT [extras].[tbl_projects] ON
GO
INSERT INTO [extras].[tbl_projects] ([id], [name]) VALUES (N'1', N'DHO')
GO
GO
INSERT INTO [extras].[tbl_projects] ([id], [name]) VALUES (N'2', N'ATEMP')
GO
GO
INSERT INTO [extras].[tbl_projects] ([id], [name]) VALUES (N'3', N'UNSOS')
GO
GO
INSERT INTO [extras].[tbl_projects] ([id], [name]) VALUES (N'4', N'FLEETrax')
GO
GO
INSERT INTO [extras].[tbl_projects] ([id], [name]) VALUES (N'5', N'TMP')
GO
GO
INSERT INTO [extras].[tbl_projects] ([id], [name]) VALUES (N'6', N'NLSTM')
GO
GO
INSERT INTO [extras].[tbl_projects] ([id], [name]) VALUES (N'7', N'UNAMA')
GO
GO
SET IDENTITY_INSERT [extras].[tbl_projects] OFF
GO

-- ----------------------------
-- Table structure for tbl_requests
-- ----------------------------
DROP TABLE [extras].[tbl_requests]
GO
CREATE TABLE [extras].[tbl_requests] (
[id] int NOT NULL IDENTITY(1,1) ,
[request_type] varchar(100) NULL ,
[requestor_id] int NULL ,
[request_status] varchar(25) NULL ,
[department] varchar(100) NULL ,
[vehicle_type] varchar(100) NULL ,
[vehicle_model] varchar(100) NULL ,
[fund_source] int NULL ,
[under_agreement] int NULL ,
[add_to_fleet] int NULL ,
[mod_require] int NULL ,
[alternative_fuel] int NULL ,
[justifications] text NULL ,
[project_site_id] int NULL ,
[request_date] datetime NULL ,
[additional_notes] text NULL ,
[modified_date] datetime NULL 
)


GO
DBCC CHECKIDENT(N'[extras].[tbl_requests]', RESEED, 30)
GO

-- ----------------------------
-- Records of tbl_requests
-- ----------------------------
SET IDENTITY_INSERT [extras].[tbl_requests] ON
GO
INSERT INTO [extras].[tbl_requests] ([id], [request_type], [requestor_id], [request_status], [department], [vehicle_type], [vehicle_model], [fund_source], [under_agreement], [add_to_fleet], [mod_require], [alternative_fuel], [justifications], [project_site_id], [request_date], [additional_notes], [modified_date]) VALUES (N'29', N'VEHICLE REQUISITION', N'118', N'Pending', N'test dept', N'test type', N'model', N'0', N'1', N'0', N'0', N'1', N'<p><span style="font-weight: bold;">sadasdsadas<br><br>dsadsa<br><br>dsadasd</span><br></p>', N'0', N'2016-07-26 00:00:00.000', null, N'2016-07-27 14:03:48.187')
GO
GO
INSERT INTO [extras].[tbl_requests] ([id], [request_type], [requestor_id], [request_status], [department], [vehicle_type], [vehicle_model], [fund_source], [under_agreement], [add_to_fleet], [mod_require], [alternative_fuel], [justifications], [project_site_id], [request_date], [additional_notes], [modified_date]) VALUES (N'30', N'VEHICLE REQUISITION', N'118', N'Approved', N'gdfgsdfgdsfg', N'dfgfdg', N'fdgs', N'1', N'1', N'1', N'1', N'1', N'<p><span style="text-decoration: underline;">sdasdasdasd</span><br></p>', N'0', N'2016-07-26 00:00:00.000', N'this is a test&nbsp;<br><br><h4 class="m-b-30 m-t-0 header-title">APPROVER COMMENT</h4>', N'2016-07-26 14:43:32.617')
GO
GO
SET IDENTITY_INSERT [extras].[tbl_requests] OFF
GO

-- ----------------------------
-- Table structure for tbl_tokens
-- ----------------------------
DROP TABLE [extras].[tbl_tokens]
GO
CREATE TABLE [extras].[tbl_tokens] (
[id] int NOT NULL IDENTITY(1,1) ,
[user_id] int NULL ,
[module] varchar(100) NULL ,
[token] varchar(250) NULL ,
[timestamp] datetime NULL ,
[used] int NULL DEFAULT ((0)) 
)


GO
DBCC CHECKIDENT(N'[extras].[tbl_tokens]', RESEED, 11)
GO

-- ----------------------------
-- Records of tbl_tokens
-- ----------------------------
SET IDENTITY_INSERT [extras].[tbl_tokens] ON
GO
INSERT INTO [extras].[tbl_tokens] ([id], [user_id], [module], [token], [timestamp], [used]) VALUES (N'9', N'118', N'RESET_PASSWORD', N'52f4c9cd-5c66-4ee6-b95b-0778f9fb91d3', N'2016-07-27 15:14:41.000', N'1')
GO
GO
INSERT INTO [extras].[tbl_tokens] ([id], [user_id], [module], [token], [timestamp], [used]) VALUES (N'10', N'118', N'RESET_PASSWORD', N'6494c02c-64cc-4d48-ad2f-6fe8dd09bc64', N'2016-07-28 15:35:07.880', N'1')
GO
GO
INSERT INTO [extras].[tbl_tokens] ([id], [user_id], [module], [token], [timestamp], [used]) VALUES (N'11', N'118', N'RESET_PASSWORD', N'99df4b53-b88c-434e-8b60-6420d1d4f076', N'2016-07-31 13:54:25.630', N'1')
GO
GO
SET IDENTITY_INSERT [extras].[tbl_tokens] OFF
GO

-- ----------------------------
-- Indexes structure for table tbl_custom_fields
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tbl_custom_fields
-- ----------------------------
ALTER TABLE [extras].[tbl_custom_fields] ADD PRIMARY KEY ([id])
GO

-- ----------------------------
-- Indexes structure for table tbl_emails
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tbl_emails
-- ----------------------------
ALTER TABLE [extras].[tbl_emails] ADD PRIMARY KEY ([id])
GO

-- ----------------------------
-- Indexes structure for table tbl_logs
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tbl_logs
-- ----------------------------
ALTER TABLE [extras].[tbl_logs] ADD PRIMARY KEY ([id])
GO

-- ----------------------------
-- Indexes structure for table tbl_options
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tbl_options
-- ----------------------------
ALTER TABLE [extras].[tbl_options] ADD PRIMARY KEY ([id])
GO

-- ----------------------------
-- Indexes structure for table tbl_project_sites
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tbl_project_sites
-- ----------------------------
ALTER TABLE [extras].[tbl_project_sites] ADD PRIMARY KEY ([id])
GO

-- ----------------------------
-- Indexes structure for table tbl_projects
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tbl_projects
-- ----------------------------
ALTER TABLE [extras].[tbl_projects] ADD PRIMARY KEY ([id])
GO

-- ----------------------------
-- Indexes structure for table tbl_requests
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tbl_requests
-- ----------------------------
ALTER TABLE [extras].[tbl_requests] ADD PRIMARY KEY ([id])
GO

-- ----------------------------
-- Indexes structure for table tbl_tokens
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tbl_tokens
-- ----------------------------
ALTER TABLE [extras].[tbl_tokens] ADD PRIMARY KEY ([id])
GO
