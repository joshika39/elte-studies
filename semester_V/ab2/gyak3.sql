CREATE OR REPLACE PROCEDURE num_of_rows IS
    CURSOR c_blocks IS
        SELECT file_id, block_id, block_id + blocks - 1 AS end_block
        FROM dba_extents
        WHERE segment_name = 'TABLA_123'
          AND owner = 'NIKOVITS'
        ORDER BY file_id, block_id;
    
    v_file_id    NUMBER;
    v_block_id   NUMBER;
    v_end_block  NUMBER;
    v_row_count  NUMBER;
BEGIN
    FOR block_rec IN c_blocks LOOP
        v_block_id := block_rec.block_id;
        v_end_block := block_rec.end_block;
        
        WHILE v_block_id <= v_end_block LOOP
            SELECT COUNT(*) INTO v_row_count
            FROM nikovits.tabla_123
            WHERE DBMS_ROWID.ROWID_BLOCK_NUMBER(rowid) = v_block_id
              AND DBMS_ROWID.ROWID_RELATIVE_FNO(rowid) = block_rec.file_id;
            DBMS_OUTPUT.PUT_LINE(block_rec.file_id || ';' || v_block_id || '->' || v_row_count || ';');
            v_block_id := v_block_id + 1;
        END LOOP;
    END LOOP;
END;
/

-- Futtatas
EXECUTE num_of_rows();

-- Teszt
EXECUTE check_plsql('num_of_rows()');
