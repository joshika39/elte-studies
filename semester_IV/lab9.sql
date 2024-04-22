SET SERVEROUTPUT ON
CREATE OR REPLACE FUNCTION nap_nev(p_kar VARCHAR2) RETURN VARCHAR2 IS
BEGIN                       
 DECLARE
  res STRING(20);
 BEGIN
  res := TO_CHAR(TO_DATE(p_kar, 'YYYY.MM.DD'), 'DAY');
  BEGIN
  EXCEPTION
    res := TO_CHAR(TO_DATE(p_kar, 'DD.MM.YYYY'), 'DAY');
  END;
  dbms_output.put_line(to_char(v1)||' -- '|| nvl(to_char(v2), 'null'));
 
 EXCEPTION
  WHEN zero_divide THEN dbms_output.put_line('zero divide');      -- 1. comment this first       
  WHEN too_many_rows THEN dbms_output.put_line('too many rows');
 END;
 dbms_output.put_line('main program');
EXCEPTION
  WHEN OTHERS THEN dbms_output.put_line(SQLCODE || ' -- ' || sqlerrm);
END;
/
