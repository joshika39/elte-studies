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
