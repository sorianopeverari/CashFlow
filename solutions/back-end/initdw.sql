CREATE TABLE public.d_time
(
    id uuid NOT NULL,
    monthh integer NOT NULL,
    dayy integer NOT NULL,
    yearr integer NOT NULL,
    PRIMARY KEY (id),
    CONSTRAINT d_time_unique_date UNIQUE (monthh, dayy, yearr)
);

ALTER TABLE IF EXISTS public.d_time
    OWNER to postgres;

CREATE TABLE IF NOT EXISTS public.fact_balance
(
    time_id uuid NOT NULL,
    transaction_amount_sum double precision,
    CONSTRAINT fact_balance_pkey PRIMARY KEY (time_id),
    CONSTRAINT fact_balance_d_time FOREIGN KEY (time_id)
        REFERENCES public.d_time (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.fact_balance
    OWNER to postgres;
