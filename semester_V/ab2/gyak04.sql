-- Kotelezo feladat a 4. gyakorlatrol

CREATE OR REPLACE PROCEDURE list_indexes(p_owner VARCHAR2, p_table VARCHAR2) IS
    CURSOR c_indexes IS
        SELECT index_name
        FROM dba_indexes
        WHERE table_owner = UPPER(p_owner)
          AND table_name = UPPER(p_table)
        ORDER BY index_name;
    
    v_index_name VARCHAR2(100);
    v_size_bytes NUMBER;
BEGIN
    FOR index_rec IN c_indexes LOOP
        v_index_name := index_rec.index_name;
        
        -- Az index méretének lekérése
        SELECT SUM(bytes)
        INTO v_size_bytes
        FROM dba_segments
        WHERE segment_name = v_index_name
          AND owner = UPPER(p_owner);
        
        -- Eredmény kiírása
        DBMS_OUTPUT.PUT_LINE(v_index_name || ': ' || v_size_bytes);
    END LOOP;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('No indexes found for table: ' || p_table);
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
/

-- Futtatas
SET SERVEROUTPUT ON;
EXECUTE list_indexes('nikovits', 'customers');
EXECUTE list_indexes('nikovits', 'cikk_iot');

-- Teszt
EXECUTE check_plsql('list_indexes(''nikovits'', ''customers'')');
EXECUTE check_plsql('list_indexes(''nikovits'', ''cikk_iot'')');
