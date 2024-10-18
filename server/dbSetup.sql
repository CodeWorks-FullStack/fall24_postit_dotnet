CREATE TABLE
  IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL PRIMARY key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) COMMENT 'User Name',
    email VARCHAR(255) UNIQUE COMMENT 'User Email',
    picture VARCHAR(255) COMMENT 'User Picture'
  ) DEFAULT charset utf8mb4 COMMENT '';

CREATE TABLE
  albums (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    title TINYTEXT NOT NULL,
    description TINYTEXT,
    coverImg TEXT NOT NULL,
    archived BOOLEAN NOT NULL DEFAULT FALSE,
    category ENUM (
      'aesthetics',
      'food',
      'games',
      'animals',
      'vibes',
      'misc'
    ) NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
  );

CREATE TABLE
  pictures (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    imgUrl TEXT NOT NULL,
    albumId INT NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (albumId) REFERENCES albums (id) ON DELETE CASCADE
  );

CREATE TABLE
  watchers (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    accountId VARCHAR(255) NOT NULL,
    albumId INT NOT NULL,
    FOREIGN KEY (accountId) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (albumId) REFERENCES albums (id) ON DELETE CASCADE,
    -- I can only watch an album one time!
    UNIQUE (accountId, albumId)
  );