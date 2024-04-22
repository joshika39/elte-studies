CREATE OR REPLACE FUNCTION nap_nev(p_kar VARCHAR2) RETURN VARCHAR2 IS
BEGIN
    RETURN TO_CHAR(TO_DATE(p_kar, 'yyyy.mm.dd'), 'DAY');
EXCEPTION
    RETURN TO_CHAR(TO_DATE(p_kar, 'dd.mm.yyyy'), 'DAY');
EXCEPTION
    RETURN TO_CHAR(TO_DATE(p_kar, 'yyyy.dd.mm'), 'DAY');
EXCEPTION
    RETURN 'rossz';
END;
