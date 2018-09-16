CREATE TABLE tag_roles (
	id int NOT NULL AUTO_INCREMENT,
	name varchar(255) NOT NULL,
	PRIMARY KEY(id)
);

CREATE TABLE tags (
	id int NOT NULL AUTO_INCREMENT,
	name varchar(255) NOT NULL,
	role int NOT NULL,
	datatype varchar(255) NOT NULL DEFAULT 'void',
	PRIMARY KEY (id),
	FOREIGN KEY (role) REFERENCES tag_roles(id)
);

CREATE TABLE images (
	hash char(64) NOT NULL,
	filename varchar(255) NOT NULL,
	time_added DATETIME NOT NULL,
	PRIMARY KEY (hash)
);

CREATE TABLE taglists (
	image_hash char(64) NOT NULL,
	tag_id int NOT NULL,
	tag_data TEXT,
	CONSTRAINT pkey PRIMARY KEY (image_hash, tag_id),
	FOREIGN KEY (image_hash) REFERENCES images(hash),
	FOREIGN KEY (tag_id) REFERENCES tags(id)
);