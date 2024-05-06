-- Ezel Oracle PL/SQL feladatok.

-- 1
/* Írjunk meg egy függvényt, amelyik eldönti egy számról, hogy prím-e. Return: igen -> 1, nem -> 0 */
-- Tesztelés:
SELECT prim(26388279066623) from dual;

CREATE OR REPLACE FUNCTION prim(n IN INTEGER) RETURN NUMBER IS
    i INTEGER;
BEGIN
    IF n <= 1 THEN
        RETURN 0;
    END IF;
    
    FOR i IN 2..SQRT(n) LOOP
        IF MOD(n, i) = 0 THEN
            RETURN 0;
        END IF;
    END LOOP;
    
    RETURN 1;
END;
/


-- 2
/* Írjunk meg egy procedúrát, amelyik kiírja az n-edik Fibonacchi számot 
   fib_1 = 0, fib_2 = 1, fib_3 = 1, fib_4 = 2 ... fib_i = a megelőző kettő összege
*/
-- Tesztelés:
set serveroutput on
execute fib(10);

CREATE OR REPLACE PROCEDURE fib(n IN INTEGER) IS
    a INTEGER := 0;
    b INTEGER := 1;
    fib_num INTEGER;
BEGIN
    IF n <= 0 THEN
        DBMS_OUTPUT.PUT_LINE('Nem létező Fibonacci szám.');
        RETURN;
    END IF;

    IF n = 1 THEN
        DBMS_OUTPUT.PUT_LINE('Fibonacci szám: 0');
        RETURN;
    END IF;

    FOR i IN 2..n LOOP
        fib_num := a + b;
        a := b;
        b := fib_num;
    END LOOP;
    
    DBMS_OUTPUT.PUT_LINE('Fibonacci szám: ' || fib_num);
END;
/

-- 3
/* Írjunk meg egy függvényt, amelyik visszaadja két szám legnagyobb közös osztóját */
-- Tesztelés:
SELECT lnko(3570,7293) FROM dual;

CREATE OR REPLACE FUNCTION lnko(p1 INTEGER, p2 INTEGER) RETURN NUMBER IS
    a INTEGER := ABS(p1);
    b INTEGER := ABS(p2);
    temp INTEGER;
BEGIN
    IF b > a THEN
        temp := a;
        a := b;
        b := temp;
    END IF;

    WHILE b != 0 LOOP
        temp := b;
        b := MOD(a, b);
        a := temp;
    END LOOP;
    
    RETURN a;
END;
/

-- 4
/* Írjunk meg egy függvényt, amelyik megadja, hogy hányszor fordul elő egy 
   karakterláncban -> p1 egy másik részkarakterlánc -> p2 
*/
-- CREATE OR REPLACE FUNCTION hanyszor(p1 VARCHAR2, p2 VARCHAR2) RETURN integer IS
-- Tesztelés:
SELECT hanyszor ('ab c ab ab de ab fg', 'ab') FROM dual;

CREATE OR REPLACE FUNCTION hanyszor(p1 VARCHAR2, p2 VARCHAR2) RETURN INTEGER IS
    cnt INTEGER := 0;
    pos INTEGER := 1;
BEGIN
    LOOP
        pos := INSTR(p1, p2, pos);
        IF pos = 0 THEN
            EXIT;
        END IF;
        cnt := cnt + 1;
        pos := pos + LENGTH(p2);
    END LOOP;
    
    RETURN cnt;
END;
/

-- 5
/* Írjunk meg egy függvényt, amelyik visszaadja a paraméterként szereplő '+'-szal 
   elválasztott számok összegét.
*/
-- CREATE OR REPLACE FUNCTION osszeg(p_char VARCHAR2) RETURN number IS
-- Tesztelés:
SELECT osszeg('1 + 4 + 13 + -1 + 0') FROM dual;

CREATE OR REPLACE FUNCTION osszeg(p_char VARCHAR2) RETURN NUMBER IS
    total NUMBER := 0;
    pos INTEGER := 1;
    len INTEGER;
    num VARCHAR2(100);
BEGIN
    len := LENGTH(p_char);
    
    WHILE pos <= len LOOP
        num := TRIM(SUBSTR(p_char, pos, INSTR(p_char, '+', pos) - pos));
        BEGIN
            total := total + TO_NUMBER(num);
        EXCEPTION
            WHEN VALUE_ERROR THEN
                NULL;
        END;
        pos := INSTR(p_char, '+', pos);
        IF pos = 0 THEN
            EXIT;
        END IF;
        pos := pos + 1;
    END LOOP;
    BEGIN
        num := TRIM(SUBSTR(p_char, pos));
        total := total + TO_NUMBER(num);
    EXCEPTION
        WHEN VALUE_ERROR THEN
            NULL;
    END;
    RETURN total;
END;
/
