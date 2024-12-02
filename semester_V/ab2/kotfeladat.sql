CREATE OR REPLACE PROCEDURE print_histogram(
   p_owner VARCHAR2,
   p_table VARCHAR2,
   p_col VARCHAR2
) IS
   v_sql           VARCHAR2(4000);
   v_distinct_count NUMBER;
   v_max_freq      NUMBER := 0;
   v_scale_factor  NUMBER := 1;
   v_error_message VARCHAR2(4000);

   TYPE freq_rec IS RECORD (
      value VARCHAR2(4000),
      freq  NUMBER
   );
   TYPE freq_table IS TABLE OF freq_rec;
   v_data freq_table;

BEGIN
   BEGIN
      EXECUTE IMMEDIATE 'SELECT COUNT(*) FROM ' || p_owner || '.' || p_table || ' WHERE 1=0';
   EXCEPTION
      WHEN OTHERS THEN
         DBMS_OUTPUT.PUT_LINE('Non-existing table or column');
         RETURN;
   END;
   BEGIN
      EXECUTE IMMEDIATE 'SELECT COUNT(DISTINCT ' || p_col || ') FROM ' || p_owner || '.' || p_table
         INTO v_distinct_count;
   EXCEPTION
      WHEN OTHERS THEN
         DBMS_OUTPUT.PUT_LINE('Error calculating distinct count');
         RETURN;
   END;
   IF v_distinct_count > 100 THEN
      DBMS_OUTPUT.PUT_LINE('Few or too many distinct values in column');
      RETURN;
   END IF;
   v_sql := 'SELECT ' || p_col || ', COUNT(*) AS freq FROM ' || p_owner || '.' || p_table ||
            ' GROUP BY ' || p_col || ' ORDER BY ' || p_col;
   EXECUTE IMMEDIATE v_sql BULK COLLECT INTO v_data;
   FOR i IN v_data.FIRST .. v_data.LAST LOOP
      IF v_data(i).freq > v_max_freq THEN
         v_max_freq := v_data(i).freq;
      END IF;
   END LOOP;
   IF v_max_freq > 50 THEN
      v_scale_factor := v_max_freq / 50;
   END IF;
   --DBMS_OUTPUT.PUT_LINE('Value Range' || RPAD(' ', 20) || '--> Frequency');
   DBMS_OUTPUT.PUT_LINE('------------------------------------------------');
   FOR i IN v_data.FIRST .. v_data.LAST LOOP
      IF v_data(i).value IS NOT NULL THEN
         DBMS_OUTPUT.PUT_LINE(
            v_data(i).value || RPAD(' ', 20 - LENGTH(v_data(i).value)) || '--> ' ||
            RPAD('*', ROUND(v_data(i).freq / v_scale_factor) + 1, '*')
         );
      END IF;
   END LOOP;

EXCEPTION
   WHEN OTHERS THEN
      v_error_message := SQLERRM;
      DBMS_OUTPUT.PUT_LINE('Error occurred: ' || v_error_message);
END;
/

