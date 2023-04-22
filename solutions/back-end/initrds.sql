CREATE TABLE IF NOT EXISTS public.transaction
(
    id uuid NOT NULL,
    effective_date timestamp without time zone NOT NULL,
    amount double precision NOT NULL,
    CONSTRAINT transaction_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.transaction
    OWNER to postgres;
