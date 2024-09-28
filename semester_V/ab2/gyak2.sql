CREATE OR REPLACE PROCEDURE newest_table(p_user VARCHAR2) IS
    v_table_name    VARCHAR2(100);
    v_size_bytes    NUMBER;
    v_creation_date DATE;
BEGIN
    SELECT table_name, created
    INTO v_table_name, v_creation_date
    FROM all_tables
    WHERE owner = UPPER(p_user)
    ORDER BY created DESC
    FETCH FIRST 1 ROWS ONLY;

    SELECT SUM(bytes)
    INTO v_size_bytes
    FROM all_segments
    WHERE segment_name = v_table_name
      AND owner = UPPER(p_user);

    DBMS_OUTPUT.PUT_LINE('Table_name: ' || v_table_name || '   Size: ' || v_size_bytes || ' bytes   Created: ' || TO_CHAR(v_creation_date, 'YYYY.MM.DD.HH24:MI'));
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('No tables found for user: ' || p_user);
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;

-- Futtatas
EXECUTE newest_table('yqmwho');


-- Teszt
EXECUTE check_plsql('newest_table(''yqmwho'')');

