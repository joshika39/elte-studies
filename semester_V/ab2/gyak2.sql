CREATE OR REPLACE PROCEDURE newest_table(p_user VARCHAR2) IS
    v_table_name    VARCHAR2(100);
    v_size_mb       NUMBER;
    v_creation_date DATE;
BEGIN
    SELECT t.table_name, o.created
    INTO v_table_name, v_creation_date
    FROM dba_tables t
    JOIN dba_objects o ON t.table_name = o.object_name
    WHERE t.owner = UPPER(p_user)
      AND o.object_type = 'TABLE'
    ORDER BY o.created DESC
    FETCH FIRST 1 ROWS ONLY;
    SELECT ROUND(SUM(s.bytes) / (1024 * 1024), 2)
    INTO v_size_mb
    FROM dba_segments s
    WHERE s.segment_name = v_table_name
      AND s.owner = UPPER(p_user)
    GROUP BY s.tablespace_name, s.segment_name;
    DBMS_OUTPUT.PUT_LINE('Table_name: ' || v_table_name || '   Size: ' || v_size_mb || ' MB   Created: ' || TO_CHAR(v_creation_date, 'YYYY.MM.DD.HH24:MI'));
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('No tables found for user: ' || p_user);
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
/

-- Futtatas
EXECUTE newest_table('YQMHWO');


-- Teszt
EXECUTE check_plsql('newest_table(''NIKOVITS'')');

