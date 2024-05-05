--1
create or replace PROCEDURE gazdag_leszarmazott IS
    tmp VARCHAR(32766);
    CURSOR emberek IS
        SELECT nev
        FROM NIKOVITS.VAGYONOK
        WHERE van_gazdag_leszarmazott(nev) > 1;
BEGIN
    OPEN emberek;
    LOOP
        FETCH emberek INTO tmp;
        EXIT WHEN emberek%notfound;
        DBMS_OUTPUT.PUT_LINE(tmp);
    END LOOP;
    CLOSE emberek;
END;
--2
create or replace PROCEDURE gazdag_leszarmazottak IS
    tmp1 VARCHAR(32766);
    tmp2 NUMBER;
    CURSOR emberek IS
        SELECT nev, vagyon
        FROM NIKOVITS.VAGYONOK
        WHERE van_gazdag_leszarmazott(nev) > 1
        ORDER BY nev;
    v_avg NUMBER;
BEGIN
    OPEN emberek;
    LOOP
        FETCH emberek INTO tmp1, tmp2;
        EXIT WHEN emberek%notfound;
        SELECT AVG(vagyon)
        INTO v_avg
        FROM (
            SELECT LPAD(' ', 2*(LEVEL-1)) || nev, vagyon
            FROM NIKOVITS.VAGYONOK
            WHERE  LEVEL > 1
            START WITH nev = tmp1
            CONNECT BY prior nev = apja AND prior vagyon < vagyon);
        DBMS_OUTPUT.PUT_LINE(tmp1 || ', ' || tmp2 || ', ' || v_avg);
    END LOOP;
    CLOSE emberek;
END;
--3
create or replace PROCEDURE kor_kereso(kezdopont VARCHAR2) IS
    CURSOR varosok IS
        SELECT SYS_CONNECT_BY_PATH(hova, '-')
        FROM NIKOVITS.JARATOK
        WHERE hova = kezdopont
        START WITH honnan = kezdopont
        CONNECT BY nocycle prior hova = honnan;
    tmp VARCHAR2(32767);
BEGIN
    OPEN varosok;
    LOOP
        FETCH varosok INTO tmp;
        EXIT WHEN varosok%notfound;
        DBMS_OUTPUT.put_line(kezdopont || tmp);
    END LOOP;
    CLOSE varosok;
END;
--4
