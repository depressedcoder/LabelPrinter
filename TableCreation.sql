-- MSSQL database table creation

CREATE TABLE LABELS_IN(
	ID bigint auto_increment primary key,
    NAME NVARCHAR(500) NOT NULL,
    WEIGHT decimal(18,2),
    LABEL NVARCHAR(8000)
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
    NAME NVARCHAR(500) NOT NULL,
    WEIGHT decimal(18,2),
    LABEL NVARCHAR(8000)
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

CREATE TABLE "LABELS_IN"
(
    "ID" NUMBER(10) NOT NULL,
    "NAME" VARCHAR2(500) NOT NULL,
    "WEIGHT" DECIMAL(10,2),
    "LABEL" VARCHAR2(4000) 
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
 







