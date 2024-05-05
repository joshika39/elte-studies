--1
create or replace PROCEDURE fiz_mod IS 
 CURSOR osztalyok IS
        SELECT oazon
        FROM nikovits.osztaly;
 v_oazon NUMBER;
 dolgozoCnt NUMBER;
 tmp_avg NUMBER;
BEGIN
    OPEN osztalyok;
    LOOP
        FETCH osztalyok INTO v_oazon;
        EXIT WHEN osztalyok%notfound;
        
        SELECT COUNT(DISTINCT dkod)
        INTO dolgozoCnt
        FROM nikovits.dolgozo 
        WHERE oazon = v_oazon;
        
        FOR v_id in (SELECT dkod 
                     FROM nikovits.dolgozo 
                     WHERE oazon = v_oazon AND belepes = (SELECT MIN(belepes) 
                                                          FROM nikovits.dolgozo d 
                                                          WHERE d.oazon = v_oazon)) 
        LOOP  
            UPDATE dolgozo
            SET fizetes = fizetes + 100 * (dolgozoCnt-1)
            WHERE dkod = v_id.dkod;
        END LOOP;
    END LOOP;
    CLOSE osztalyok;  
    
    COMMIT;

    OPEN osztalyok;
    LOOP
       FETCH osztalyok INTO v_oazon;
       EXIT WHEN osztalyok%notfound;

       SELECT AVG(fizetes)
       INTO tmp_avg
       FROM dolgozo
       WHERE oazon = v_oazon;

       DBMS_OUTPUT.put_line(v_oazon || ': ' || tmp_avg);
    END LOOP;
    CLOSE osztalyok;  

    ROLLBACK;
END;
--2
create or replace PROCEDURE curs_upd(p_oazon INTEGER) IS 
    CURSOR dolgozok IS
        SELECT d.dkod, d.dnev
        FROM dolgozo d JOIN osztaly o ON d.oazon = o.oazon 
        WHERE o.oazon = p_oazon;
    v_dkod dolgozo.dkod%TYPE;
    v_dnev dolgozo.dnev%TYPE;
    tmp INTEGER;
BEGIN
    OPEN dolgozok;
    LOOP
        FETCH dolgozok INTO v_dkod, v_dnev;
        EXIT WHEN dolgozok%notfound;

        --regexp_count(LOWER(v_dnev), '[aeiou]', 1)
        SELECT fizetes INTO tmp FROM dolgozo WHERE dkod = v_dkod;
        DBMS_OUTPUT.put(v_dnev || ': ' || tmp || ' --> ');
        UPDATE dolgozo
        SET fizetes = fizetes + regexp_count(LOWER(v_dnev), '[aeiou]', 1) * 10
        WHERE dkod = v_dkod;
        SELECT fizetes INTO tmp FROM dolgozo WHERE dkod = v_dkod;
        DBMS_OUTPUT.put_line(tmp);
    END LOOP;
    CLOSE DOLGOZOK;
    
    COMMIT;
    
    ROLLBACK;
END;
--3
create or replace FUNCTION nap_nev(p_kar VARCHAR2) RETURN VARCHAR2 IS
    datum DATE;
    asd EXCEPTION;
BEGIN
    datum := TO_DATE(p_kar, 'yyyy.mm.dd');
    RETURN TO_CHAR(datum, 'Day', 'nls_date_language=hungarian');
    EXCEPTION WHEN OTHERS THEN
        BEGIN
            datum := TO_DATE(p_kar, 'dd.mm.yyyy');
            RETURN TO_CHAR(datum, 'Day', 'nls_date_language=hungarian');
            EXCEPTION WHEN OTHERS THEN
                RETURN 'rossz;';
        END;
END;
--4
create or replace PROCEDURE szamok(n number) IS
BEGIN
    --SQRT(num)
    DBMS_OUTPUT.put_line('Reciprok: ' || 1/n);
    DBMS_OUTPUT.put_line('sqrt: ' || TO_CHAR(SQRT(n), 'fm99D00'));
    DBMS_OUTPUT.put_line('Faktor: ' || faktor(n));
    EXCEPTION WHEN OTHERS THEN
         DBMS_OUTPUT.put_line('Hiba kód: ' || SQLCODE);
END;
--5
create or replace FUNCTION osszeg2(delimited_string VARCHAR2) RETURN number IS
    delimiter CHAR := '+';
    start_index NUMBER := 1;
    end_index NUMBER;
    item NUMBER;
    v_sum NUMBER := 0;
BEGIN
    LOOP
        end_index := INSTR(delimited_string, delimiter, start_index);
        BEGIN
            IF end_index = 0 THEN
                item := TO_NUMBER(TRIM(SUBSTR(delimited_string, start_index)));
            ELSE 
                item := TO_NUMBER(TRIM(SUBSTR(delimited_string, start_index, end_index - start_index)));
            END IF;
        EXCEPTION WHEN OTHERS THEN
            item := 0;
        END;
        IF (item IS NULL) THEN
            item := 0;
        END IF;
        --DBMS_OUTPUT.PUT_LINE(item); 
        v_sum := v_sum + item;
        start_index := end_index + 1;
        EXIT WHEN end_index = 0;
    END LOOP;
    RETURN v_sum;
END;
