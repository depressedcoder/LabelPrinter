-- MSSQL database table creation


CREATE TABLE [dbo].[LABELS_IN](
	[ID] [bigint] IDENTITY(1,1) primary key NOT NULL,
	[NAME] [nvarchar](500) NOT NULL,
	[WEIGHT] [decimal](18, 2) NULL,
	[Date] [datetime] NULL,
	[Line1] [nvarchar](200) NULL,
	[Line2] [nvarchar](200) NULL,
	[Line3] [nvarchar](200) NULL,
	[Line4] [nvarchar](200) NULL,
	[Line5] [nvarchar](200) NULL,
	[Line6] [nvarchar](200) NULL,
	[Line7] [nvarchar](200) NULL,
	[Line8] [nvarchar](200) NULL,
	[Line9] [nvarchar](200) NULL,
	[Line10] [nvarchar](200) NULL,
	[Line11] [nvarchar](200) NULL,
	[Line12] [nvarchar](200) NULL,
	[Line13] [nvarchar](200) NULL,
	[Line14] [nvarchar](200) NULL,
	[Line15] [nvarchar](200) NULL
)

CREATE TABLE LABELS_OUT(
	ID bigint auto_increment primary key,
    NAME NVARCHAR(500) NOT NULL,
    WEIGHT decimal(18,2),
    LABEL NVARCHAR(8000)
) 
 

--END

-- MySQL database table creation

CREATE TABLE LABELS_IN(
	ID bigint auto_increment primary key,
	NAME nvarchar(500) NOT NULL,
	WEIGHT decimal(18, 2) NULL,
	Date datetime NULL,
	Line1 nvarchar(200) NULL,
	Line2 nvarchar(200) NULL,
	Line3 nvarchar(200) NULL,
	Line4 nvarchar(200) NULL,
	Line5 nvarchar(200) NULL,
	Line6 nvarchar(200) NULL,
	Line7 nvarchar(200) NULL,
	Line8 nvarchar(200) NULL,
	Line9 nvarchar(200) NULL,
	Line10 nvarchar(200) NULL,
	Line11 nvarchar(200) NULL,
	Line12 nvarchar(200) NULL,
	Line13 nvarchar(200) NULL,
	Line14 nvarchar(200) NULL,
	Line15 nvarchar(200) NULL
)
 

CREATE TABLE LABELS_OUT(
	ID bigint auto_increment primary key,
    NAME NVARCHAR(500) NOT NULL,
    WEIGHT decimal(18,2),
    LABEL NVARCHAR(8000)
) 
--END

--Oracle database table creation

CREATE TABLE "LABELS_OUT"
(
    "ID" NUMBER(10) NOT NULL,
    "NAME" VARCHAR2(500) NOT NULL,
    "WEIGHT" DECIMAL(10),
    "LABEL" VARCHAR2(4000),
	CONSTRAINT "label_pk" PRIMARY KEY("ID"))
)


 ALTER TABLE "LABELS_OUT" ADD (
  CONSTRAINT "label_pk" PRIMARY KEY("ID"))
  
   CREATE SEQUENCE "label_seq" START WITH 1;

CREATE OR REPLACE TRIGGER "label_bir"
BEFORE INSERT ON "LABELS_OUT" 
FOR EACH ROW
BEGIN
  SELECT "label_seq".NEXTVAL
  INTO   :new.id
  FROM   dual;
END;


CREATE TABLE LABELS_IN (
	"ID"  NUMBER(10) NOT NULL,
	"NAME" varchar2(500)  NOT NULL,
	"WEIGHT" decimal(18, 2) NULL,
	"Date" DATE NULL,
	"Line1" varchar2(200) NULL,
	"Line2" varchar2(200) NULL,
	"Line3" varchar2(200) NULL,
	"Line4" varchar2(200) NULL,
	"Line5" varchar2(200) NULL,
	"Line6" varchar2(200) NULL,
	"Line7" varchar2(200) NULL,
	"Line8" varchar2(200) NULL,
	"Line9" varchar2(200) NULL,
	"Line10" varchar2(200) NULL,
	"Line11" varchar2(200) NULL,
	"Line12" varchar2(200) NULL,
	"Line13" varchar2(200) NULL,
	"Line14" varchar2(200) NULL,
	"Line15" varchar2(200) NULL
)
 
 ALTER TABLE "LABELS_IN" ADD (
  CONSTRAINT "label_in_pk" PRIMARY KEY("ID"))

 CREATE SEQUENCE "label_in_seq" START WITH 1;

CREATE OR REPLACE TRIGGER "label_in_bir"
BEFORE INSERT ON "LABELS_IN" 
FOR EACH ROW
BEGIN
  SELECT "label_in_seq".NEXTVAL
  INTO   :new.id
  FROM   dual;
END;
--find hosted database server name
select sys_context('USERENV','SERVER_HOST') from dual

--END

INSERT INTO  "LABELS_IN" ("NAME", "WEIGHT",  "LABEL") VALUES('Oracle test', 12.45, '{"Rows":[{"Text":"Norsel Oracle","SelectedCharWidth":12,"IsHigh":true,"IsBold":false,"IsUnderlined":false,"CharWidth":24,"IsInDesignMode":false},{"Text":"<TIME>","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"<IMGNorsel>","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false},{"Text":"","SelectedCharWidth":6,"IsHigh":false,"IsBold":false,"IsUnderlined":false,"CharWidth":6,"IsInDesignMode":false}],"HowManyCoppies":1,"IsAutomaticCuttingDevice":true,"LabelWidth":120,"LabelHeight":100,"DistanceFromLeft":0,"LabelSource":null,"SelectedLabelName":"Norsel","Barcode":{"SelectedBarCode":"EAN13","CodeSize":2,"HeightOfCode":5,"IsInDesignMode":false},"IsInDesignMode":false}')

SELECT * FROM LABELS_IN
SELECT * FROM LABELS_OUT
 







