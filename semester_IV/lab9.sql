-- Correct funcion

CREATE OR REPLACE FUNCTION nap_nev(p_kar VARCHAR2) RETURN VARCHAR2 IS
  res VARCHAR2(20);
BEGIN
  BEGIN
    -- Try parsing the date with 'YYYY.MM.DD' format
    res := TO_CHAR(TO_DATE(p_kar, 'YYYY.MM.DD'), 'DAY');
  EXCEPTION
    WHEN OTHERS THEN
      BEGIN
        -- If the first format fails, try 'DD.MM.YYYY' format
        res := TO_CHAR(TO_DATE(p_kar, 'DD.MM.YYYY'), 'DAY');
      EXCEPTION
        -- Handle any other exceptions if necessary
        WHEN OTHERS THEN
          -- If both formats fail, set the result to NULL or handle the exception accordingly
          res := NULL; -- You may modify this part as per your requirement
      END;
  END;

  RETURN res;
EXCEPTION
  WHEN OTHERS THEN
    -- Handle exceptions if needed
    RETURN NULL; -- You may modify this part as per your requirement
END;
/

-- Usage

CREATE TABLE GYAK10 AS SELECT dnev, belepes, nap_nev(belepes) as belepes_nap FROM nikovits.dolgozo
WHERE fizetes > (
    SELECT AVG(fizetes)
    FROM nikovits.dolgozo
);
