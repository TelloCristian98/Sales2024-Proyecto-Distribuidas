--
-- PostgreSQL database dump
--

-- Dumped from database version 14.15 (Homebrew)
-- Dumped by pg_dump version 14.15 (Homebrew)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: categories; Type: TABLE; Schema: public; Owner: root
--

CREATE TABLE public.categories (
    categoryid integer NOT NULL,
    categoryname character varying(100) NOT NULL,
    description character varying(255)
);


ALTER TABLE public.categories OWNER TO root;

--
-- Name: categories_categoryid_seq; Type: SEQUENCE; Schema: public; Owner: root
--

CREATE SEQUENCE public.categories_categoryid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.categories_categoryid_seq OWNER TO root;

--
-- Name: categories_categoryid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: root
--

ALTER SEQUENCE public.categories_categoryid_seq OWNED BY public.categories.categoryid;


--
-- Name: comentarios; Type: TABLE; Schema: public; Owner: root
--

CREATE TABLE public.comentarios (
    comentarioid integer NOT NULL,
    publicacionid integer NOT NULL,
    autor character varying(100) NOT NULL,
    contenido character varying(500) NOT NULL,
    fecha timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public.comentarios OWNER TO root;

--
-- Name: comentarios_comentarioid_seq; Type: SEQUENCE; Schema: public; Owner: root
--

CREATE SEQUENCE public.comentarios_comentarioid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.comentarios_comentarioid_seq OWNER TO root;

--
-- Name: comentarios_comentarioid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: root
--

ALTER SEQUENCE public.comentarios_comentarioid_seq OWNED BY public.comentarios.comentarioid;


--
-- Name: products; Type: TABLE; Schema: public; Owner: root
--

CREATE TABLE public.products (
    productid integer NOT NULL,
    productname character varying(100) NOT NULL,
    categoryid integer NOT NULL,
    unitprice numeric(10,2) NOT NULL,
    unitsinstock integer NOT NULL
);


ALTER TABLE public.products OWNER TO root;

--
-- Name: products_productid_seq; Type: SEQUENCE; Schema: public; Owner: root
--

CREATE SEQUENCE public.products_productid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.products_productid_seq OWNER TO root;

--
-- Name: products_productid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: root
--

ALTER SEQUENCE public.products_productid_seq OWNED BY public.products.productid;


--
-- Name: categories categoryid; Type: DEFAULT; Schema: public; Owner: root
--

ALTER TABLE ONLY public.categories ALTER COLUMN categoryid SET DEFAULT nextval('public.categories_categoryid_seq'::regclass);


--
-- Name: comentarios comentarioid; Type: DEFAULT; Schema: public; Owner: root
--

ALTER TABLE ONLY public.comentarios ALTER COLUMN comentarioid SET DEFAULT nextval('public.comentarios_comentarioid_seq'::regclass);


--
-- Name: products productid; Type: DEFAULT; Schema: public; Owner: root
--

ALTER TABLE ONLY public.products ALTER COLUMN productid SET DEFAULT nextval('public.products_productid_seq'::regclass);


--
-- Data for Name: categories; Type: TABLE DATA; Schema: public; Owner: root
--

COPY public.categories (categoryid, categoryname, description) FROM stdin;
1	Electrónica	Dispositivos electrónicos y gadgets
2	Hogar	Artículos para el hogar
3	Musica	Productos musicales
\.


--
-- Data for Name: comentarios; Type: TABLE DATA; Schema: public; Owner: root
--

COPY public.comentarios (comentarioid, publicacionid, autor, contenido, fecha) FROM stdin;
1	101	John Doe	This is a great post!	2024-12-08 14:30:00
5	102	Jane Smith	I completely agree with your point!	2024-12-09 12:05:42.465957
6	106	Cristian Tello	This is a test	2024-12-09 12:15:33.466373
7	107	Carlos Andrango	This is a test registro	2024-12-09 12:18:49.527985
\.


--
-- Data for Name: products; Type: TABLE DATA; Schema: public; Owner: root
--

COPY public.products (productid, productname, categoryid, unitprice, unitsinstock) FROM stdin;
1	Televisor	1	599.99	20
3	Sofá	2	299.99	5
4	IKEA Table	3	999.00	5
2	Smartphone Pro	1	900.00	8
\.


--
-- Name: categories_categoryid_seq; Type: SEQUENCE SET; Schema: public; Owner: root
--

SELECT pg_catalog.setval('public.categories_categoryid_seq', 3, true);


--
-- Name: comentarios_comentarioid_seq; Type: SEQUENCE SET; Schema: public; Owner: root
--

SELECT pg_catalog.setval('public.comentarios_comentarioid_seq', 5, true);


--
-- Name: products_productid_seq; Type: SEQUENCE SET; Schema: public; Owner: root
--

SELECT pg_catalog.setval('public.products_productid_seq', 4, true);


--
-- Name: categories categories_pkey; Type: CONSTRAINT; Schema: public; Owner: root
--

ALTER TABLE ONLY public.categories
    ADD CONSTRAINT categories_pkey PRIMARY KEY (categoryid);


--
-- Name: comentarios comentarios_pkey; Type: CONSTRAINT; Schema: public; Owner: root
--

ALTER TABLE ONLY public.comentarios
    ADD CONSTRAINT comentarios_pkey PRIMARY KEY (comentarioid);


--
-- Name: products products_pkey; Type: CONSTRAINT; Schema: public; Owner: root
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (productid);


--
-- Name: products fk_products_categories; Type: FK CONSTRAINT; Schema: public; Owner: root
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT fk_products_categories FOREIGN KEY (categoryid) REFERENCES public.categories(categoryid) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

