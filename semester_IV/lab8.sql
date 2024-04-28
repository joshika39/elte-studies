
CREATE OR REPLACE FUNCTION get_jobs(o_nev VARCHAR2) RETURN VARCHAR2 IS
  v_jobs VARCHAR2(4000); -- Maximum length for the concatenated job positions
  v_oazon NUMBER;
BEGIN
  SELECT oazon INTO v_oazon FROM nikovits.osztaly WHERE onev = o_nev;
  v_jobs := '';
  FOR emp_rec IN (SELECT foglalkozas FROM nikovits.dolgozo WHERE oazon = v_oazon) LOOP
    v_jobs := v_jobs || emp_rec.foglalkozas || '-';
  END LOOP;
  IF LENGTH(v_jobs) > 0 THEN
    v_jobs := SUBSTR(v_jobs, 1, LENGTH(v_jobs) - 1);
  END IF;
  RETURN v_jobs;
END;
/

CREATE TABLE GYAK9 (
  osztaly_nev VARCHAR2(100),
  foglalkozas_nevek VARCHAR2(100)
);

INSERT INTO GYAK9 (osztaly_nev, foglalkozas_nevek)
VALUES ('ACCOUNTING', get_jobs('ACCOUNTING'));

INSERT INTO GYAK9 (osztaly_nev, foglalkozas_nevek)
VALUES ('RESEARCH', get_jobs('RESEARCH'));

INSERT INTO GYAK9 (osztaly_nev, foglalkozas_nevek)
VALUES ('SALES', get_jobs('SALES'));
