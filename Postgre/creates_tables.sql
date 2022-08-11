CREATE SCHEMA IF NOT EXISTS project;

CREATE TABLE IF NOT EXISTS project.currencies
(
    name text COLLATE pg_catalog."default",
    code text COLLATE pg_catalog."default" NOT NULL,
    rate double precision NOT NULL,
    date date NOT NULL,
    id serial NOT NULL,
    CONSTRAINT currencies_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS project.currencies
    OWNER to postgres;