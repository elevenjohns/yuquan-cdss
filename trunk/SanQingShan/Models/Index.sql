USE [CP]
GO

/****** CP_ID is not PK, but we often use it as PK, so make clustered index for it. ******/

/****** Object:  Index [IDX_CP_ID]    Script Date: 01/10/2013 11:25:15 ******/
CREATE UNIQUE CLUSTERED INDEX [IDX_CP_ID] ON [dbo].[CP_VISIT](CP_ID) 

CREATE CLUSTERED INDEX [IDX_CP_ID] ON [dbo].[CP_EXAM](CP_ID) 
CREATE CLUSTERED INDEX [IDX_CP_ID] ON [dbo].[CP_ORDER](CP_ID) 
CREATE CLUSTERED INDEX [IDX_CP_ID] ON [dbo].[CP_LAB_TEST](CP_ID) 
CREATE CLUSTERED INDEX [IDX_CP_ID] ON [dbo].[CP_VITAL_SIGN](CP_ID) 
CREATE CLUSTERED INDEX [IDX_CP_ID] ON [dbo].[CP_MEDICAL_DOC](CP_ID) 