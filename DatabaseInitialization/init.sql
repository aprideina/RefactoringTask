-- CREATE DEPARTMENT TABLE
CREATE TABLE deps (
    id         INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name       VARCHAR(40) NOT NULL,
    active     BOOL NOT NULL
);

-- CREATE EMPLOYEE TABLE
CREATE TABLE emps (
    id              INT GENERATED ALWAYS AS IDENTITY,
    name            VARCHAR(40) NOT NULL,
    inn             VARCHAR(40) NOT NULL,
    departmentid    INT REFERENCES deps(ID)
);