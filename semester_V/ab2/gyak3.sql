SET SERVEROUTPUT ON;

CREATE OR REPLACE PROCEDURE NUM_OF_ROWS IS
    CURSOR C_BLOCKS IS
    SELECT
        FILE_ID,
        BLOCK_ID,
        BLOCK_ID + BLOCKS - 1 AS END_BLOCK
    FROM
        DBA_EXTENTS
    WHERE
        SEGMENT_NAME = 'TABLA_123'
        AND OWNER = 'NIKOVITS'
    ORDER BY
        FILE_ID,
        BLOCK_ID;
    V_FILE_ID   NUMBER;
    V_BLOCK_ID  NUMBER;
    V_END_BLOCK NUMBER;
    V_ROW_COUNT NUMBER;
BEGIN
    FOR BLOCK_REC IN C_BLOCKS LOOP
        V_BLOCK_ID := BLOCK_REC.BLOCK_ID;
        V_END_BLOCK := BLOCK_REC.END_BLOCK;
        WHILE V_BLOCK_ID <= V_END_BLOCK LOOP
            SELECT
                COUNT(*) INTO V_ROW_COUNT
            FROM
                NIKOVITS.TABLA_123
            WHERE
                DBMS_ROWID.ROWID_BLOCK_NUMBER(ROWID) = V_BLOCK_ID
                AND DBMS_ROWID.ROWID_RELATIVE_FNO(ROWID) = BLOCK_REC.FILE_ID;
            DBMS_OUTPUT.PUT_LINE(BLOCK_REC.FILE_ID
                                 || ';'
                                 || V_BLOCK_ID
                                 || '->'
                                 || V_ROW_COUNT
                                 || ';');
            V_BLOCK_ID := V_BLOCK_ID + 1;
        END LOOP;
    END LOOP;
END;
/

-- Futtatas
EXECUTE num_of_rows();

-- Teszt
EXECUTE check_plsql('num_of_rows()');
