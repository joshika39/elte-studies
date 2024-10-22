SELECT BLOCKS FROM DBA_TABLES WHERE OWNER = 'VZOLI' AND TABLE_NAME = 'CIKK';

SELECT OWNER, SEGMENT_NAME, EXTENTS
FROM DBA_SEGMENTS
WHERE SEGMENT_TYPE = 'TABLE'
AND PARTITION_NAME IS NULL
ORDER BY BYTES DESC
FETCH FIRST 1 ROWS ONLY;

CREATE TABLE test_table (
    oszlop1 VARCHAR2(100),
    oszlop2 NUMBER,
    oszlop3 DATE,
    oszlop4 VARCHAR2(200),
    oszlop5 NUMBER
);

SELECT owner, table_name
FROM all_tab_columns
WHERE 
    (owner, table_name) IN (
        SELECT owner, table_name
        FROM all_tab_columns
        WHERE column_id = 1
        AND data_type = 'VARCHAR2'
    )
    AND (owner, table_name) IN (
        SELECT owner, table_name
        FROM all_tab_columns
        WHERE column_id = 4
        AND data_type = 'VARCHAR2'
    )
GROUP BY owner, table_name;

CREATE OR REPLACE PROCEDURE cr_tab(p_owner VARCHAR2, p_tabla VARCHAR2) IS
    v_sql VARCHAR2(32767);
    first_column BOOLEAN := TRUE;
BEGIN
    v_sql := 'CREATE TABLE ' || UPPER(p_owner) || '.' || UPPER(p_tabla) || ' (' || CHR(10);
    FOR rec IN (
        SELECT column_name, data_type, data_length, data_precision, data_scale, data_default
        FROM all_tab_columns
        WHERE owner = UPPER(p_owner)
        AND table_name = UPPER(p_tabla)
        AND data_type IN ('CHAR', 'VARCHAR2', 'NCHAR', 'NVARCHAR2', 'BLOB', 'CLOB', 'NCLOB', 'NUMBER', 'FLOAT', 'BINARY_FLOAT', 'DATE', 'ROWID', 'BINARY_DOUBLE')
        ORDER BY column_id
    ) LOOP
        IF NOT first_column THEN
            v_sql := v_sql || ',' || CHR(10);
        ELSE
            first_column := FALSE;
        END IF;
        v_sql := v_sql || '    ' || rec.column_name || ' ' || rec.data_type;
        IF rec.data_type IN ('CHAR', 'VARCHAR2', 'NCHAR', 'NVARCHAR2') THEN
            v_sql := v_sql || '(' || rec.data_length || ')';
        ELSIF rec.data_type = 'NUMBER' THEN
            IF rec.data_precision IS NOT NULL THEN
                v_sql := v_sql || '(' || rec.data_precision;
                IF rec.data_scale IS NOT NULL THEN
                    v_sql := v_sql || ',' || rec.data_scale;
                END IF;
                v_sql := v_sql || ')';
            END IF;
        ELSIF rec.data_type = 'FLOAT' THEN
            IF rec.data_precision IS NOT NULL THEN
                v_sql := v_sql || '(' || rec.data_precision || ')';
            END IF;
        END IF;
        IF rec.data_default IS NOT NULL THEN
            v_sql := v_sql || ' DEFAULT ' || rec.data_default;
        END IF;
    END LOOP;
    v_sql := v_sql || CHR(10) || ');';
    DBMS_OUTPUT.PUT_LINE(v_sql);
END cr_tab;
/

SET SERVEROUTPUT ON;

EXEC cr_tab('YQMHWO', 'tipus_proba');

CREATE TABLE tipus_proba(c10 CHAR(10) DEFAULT 'bubu', vc20 VARCHAR2(20), nc10 NCHAR(10),
nvc15 NVARCHAR2(15), blo BLOB, clo CLOB, nclo NCLOB, num NUMBER, num10_2 NUMBER(10,2),
num10 NUMBER(10) DEFAULT 100, flo FLOAT, bin_flo binary_float DEFAULT '2e+38',
bin_doub binary_double DEFAULT 2e+40,
dat DATE DEFAULT TO_DATE('2007.01.01', 'yyyy.mm.dd'), rid ROWID);