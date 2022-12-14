USE [DigitalBooks]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 26-09-2022 12:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[BookId] [int] NOT NULL,
	[BookName] [varchar](500) NULL,
	[CategoryId] [int] NULL,
	[Price] [decimal](18, 0) NULL,
	[Publisher] [varchar](500) NULL,
	[UserId] [int] NULL,
	[publishedDate] [date] NULL,
	[Description] [ntext] NULL,
	[Active] [bit] NULL,
	[CreatedDate] [date] NULL,
	[Createdby] [int] NULL,
	[ModifiedDate] [date] NULL,
	[Modifiedby] [int] NULL,
 CONSTRAINT [PK_BookDetails] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 26-09-2022 12:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] NOT NULL,
	[CategoryName] [varchar](50) NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 26-09-2022 12:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[PurchaseId] [int] NOT NULL,
	[EmailId] [varchar](500) NULL,
	[BookId] [int] NULL,
	[PurchaseDate] [date] NULL,
	[PaymentMode] [varchar](100) NULL,
 CONSTRAINT [PK_PaymentInfo] PRIMARY KEY CLUSTERED 
(
	[PurchaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleMaster]    Script Date: 26-09-2022 12:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMaster](
	[RoleId] [int] NOT NULL,
	[RoleName] [varchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 26-09-2022 12:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] NOT NULL,
	[UserName] [varchar](500) NULL,
	[EmailId] [varchar](500) NULL,
	[Password] [nvarchar](500) NULL,
	[RoleId] [int] NULL,
	[Active] [bit] NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (2, N'TomandJerry', 1, CAST(500 AS Decimal(18, 0)), N'Disney', 3, CAST(N'2022-09-12' AS Date), N'rjghejhgje', 1, CAST(N'2022-09-13' AS Date), 3, CAST(N'2022-09-13' AS Date), 3)
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (3, N'Harry Potter', 2, CAST(2500 AS Decimal(18, 0)), N'MaryLand', 3, CAST(N'2022-09-12' AS Date), N'gryewgrfvssdfvsdfvs', 0, CAST(N'2022-09-13' AS Date), 3, CAST(N'2022-09-21' AS Date), 3)
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (4, N'Alice in Wonderland', 2, CAST(600 AS Decimal(18, 0)), N'Lewis Carrol', 3, CAST(N'2022-09-13' AS Date), N'Alice sits on a riverbank on a warm summer day, drowsily reading over her sister’s shoulder, when she catches sight of a White Rabbit in a waistcoat running by her. The White Rabbit pulls out a pocket watch, exclaims that he is late, and pops down a rabbit hole. Alice follows the White Rabbit down the hole and comes upon a great hallway lined with doors. She finds a small door that she opens using a key she discovers on a nearby table. Through the door, she sees a beautiful garden, and Alice begins to cry when she realizes she cannot fit through the door. She finds a bottle marked “DRINK ME and downs the contents. She shrinks down to the right size ', 1, CAST(N'2022-09-13' AS Date), 3, CAST(N'2022-09-14' AS Date), 3)
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (5, N'Discovery of India', 2, CAST(500 AS Decimal(18, 0)), N'nmsdbvnbas', 3, CAST(N'2022-09-13' AS Date), N'eqwjgejhqweghj nvfbfvwefvwefv', 0, CAST(N'2022-09-13' AS Date), 3, CAST(N'2022-09-14' AS Date), 3)
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (6, N'Two states', 2, CAST(750 AS Decimal(18, 0)), N'Chetan Bhagat', 3, CAST(N'2022-09-13' AS Date), N'The Story of My Marriage is autobiographical with only names changed. The story is about a couple Krish and Ananya, who hail from two states of India, Punjab and Tamil Nadu, respectively, who are deeply in love and want to marry. It is narrated from a first-person point of view in a humorous tone, often taking digs at both Tamil and Punjabi cultures.  The story begins in the IIM Ahmedabad mess hall where Krish, a Punjabi boy from Delhi sights a beautiful girl Ananya, a Tamilian from Chennai quarreling with the mess staff about the food. Ananya was tagged as the "Best girl of the fresher batch". They become friends within a few days. Both graduate and get jobs with serious plans for their wedding. At first, Krish tries to convince his girlfriend Ananya''s parents by helping Ananya''s brother Manju IIT tuition and by helping her father Swaminathan create his first PowerPoint presentation. He later convinces her mom by helping her fulfill her biggest dream of singing at a concert by arranging for her to perform at the concert organised by Krish''s employer City Bank.  With Ananya''s parents convinced, the couple then has to convince Krish''s mother. But they run into problems as Krish''s mother''s relatives don''t quite like the relationship and do not want Krish to marry a Tamilian. They are won over after Ananya intervenes to help one of Krish''s cousins get married. Now as they have convinced both their parents, they decide to make a trip to Goa to give their parents an opportunity to get to know each other. But this too ends badly as Ananya''s parents have a fallout with Krish''s mother after which they leave, deciding that the families can never get along. Krish returns home and becomes a depressed workaholic.  Throughout the story, Krish was not on good terms with his father and doesn''t share a close bond with him. But finally, it is revealed that Krish''s father travels to Chennai to meet Ananya''s parents and successfully convinces them by spending a day with them. Thus, father and son are reconciled and the novel ends with Ananya giving birth to twin boys. Krish says that the babies belong to a state called ''India'', with a thought to end inequality.', 1, CAST(N'2022-09-14' AS Date), 3, CAST(N'2022-09-14' AS Date), 3)
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (7, N'Mr Bean', 1, CAST(800 AS Decimal(18, 0)), N'Chetan Bhagat', 3, CAST(N'2022-09-13' AS Date), N'Mr. Bean is a British sitcom created by Rowan Atkinson and Richard Curtis, produced by Tiger Aspect and starring Atkinson as the title character. The sitcom consists of 15 episodes that were co-written by Atkinson alongside Curtis and Robin Driscoll; for the pilot, it was co-written by Ben Elton. The series was originally broadcast on ITV, beginning with the pilot on 1 January 1990[1] and ending with "The Best Bits of Mr. Bean" on 15 December 1995.  Based on a character originally developed by Atkinson while he was studying for his master''s degree at the University of Oxford, the series centres on Mr. Bean, described by Atkinson as "a child in a grown man''s body", as he solves various problems presented by everyday tasks and often causes disruption in the process.[2] The series has been influenced by physical comedy actors such as Jacques Tati and those from early silent films.[2]  During its original five-year run, Mr. Bean met with widespread acclaim and attracted large television audiences. The series was viewed by 18.74 million viewers for the episode "The Trouble with Mr. Bean"[3] and has received a number of international awards, including the Rose d''Or. The series has since been sold in 245 territories worldwide. It has inspired an animated spin-off and two theatrical feature-length films along with Atkinson reprising his role as Mr. Bean for a performance at the London 2012 Summer Olympics opening ceremony, television commercials and several sketches for Comic Relief. The programme carries strong appeal in hundreds of territories worldwide because, in addition to the acclaim from its original run, it uses very little intelligible dialogue, making it accessible to people who know little or no English.', 0, CAST(N'2022-09-14' AS Date), 3, CAST(N'2022-09-14' AS Date), 3)
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (8, N'Angry Birds Comics Volume 3: Sky High', 1, CAST(850 AS Decimal(18, 0)), N'Paul Tobin', 3, CAST(N'2022-09-14' AS Date), N'The adventures continue in these charming all-ages comics featuring, who else, the Angry Birds! Sky High contains all-new stories based on the best-selling game!', 1, CAST(N'2022-09-14' AS Date), 3, CAST(N'2022-09-14' AS Date), 3)
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (9, N'DuckTales: Quests and Quacks', 1, CAST(850 AS Decimal(18, 0)), N'Joey Cavalieri', 3, CAST(N'2022-09-14' AS Date), N'Tales from the heart of Duckburg and beyond! This volume collects stories inspired by the critically acclaimed Disney Channel series, as Scrooge and company face off against brand new threats, and a few familiar ones too!  In these adventures inspired by the hit series, Scrooge and company deal with magical relics, dastardly ghosts, and one of Scrooge''s greatest threats: a risk assessor from an insurance company! Plus, see the return of classic villains The Beagle Boys!  This volume features six new tales from writers Joe Caramagna and Joey Cavalieri, and collects issues #6-8 of the ongoing series.', 1, CAST(N'2022-09-14' AS Date), 3, CAST(N'2022-09-14' AS Date), 3)
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (10, N'Scooby-Doo', 1, CAST(300 AS Decimal(18, 0)), N'Sholly Fisch', 5, CAST(N'2022-09-14' AS Date), N'ZOINKS! Get ready for more groovy team-ups of Scooby and he gang in Scooby-Doo Team-Up: It''s Scooby Time!  Scooby-Doo and the whole mystery-solving gang are back at it again with these collaborative capers! Over the years, Scooby and the gang have nabbed enough nefarious villains to fill a rogues gallery. But the tables are turned when they have to help a rogues gallery of the Flash''s most malicious super-villains! Can the Scooby gang rescue Flash''s Rogues from the ghost of that sinister spinner, the Top? Plus, Dastardly and Muttley, Black Lightning, Batman and more!  Check out all these zany adventures in Scooby-Doo Team-Up: It''s Scooby Time! From writer Sholly Fisch (Teen Titans Go!) and artist Dario Brizuela (Green Lantern: The Animated Series), these wacky capers are sure to delight readers of all ages. Collects Scooby-Doo Team-Up #44-50.', 1, CAST(N'2022-09-14' AS Date), 5, CAST(N'2022-09-14' AS Date), 5)
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (11, N'The Loud House #1: There Will Be Chaos', 1, CAST(50 AS Decimal(18, 0)), N'Nickelodeon', 5, CAST(N'2022-09-14' AS Date), N'Ever wonder what it’s like having a big family? 11-year-old Lincoln Loud lives with his 10 sisters. The trick to surviving the chaos is to remain calm, cool, and collected. But most importantly for Lincoln, you’ve got to have a plan. With all the chaos, and craziness, one thing is always for sure: there is never a dull moment in the Loud house! All-new stories from Nickelodeon’s newest hit-series, created by Chris Savino.', 0, CAST(N'2022-09-14' AS Date), 5, CAST(N'2022-09-15' AS Date), 5)
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (12, N'Cindrella', 2, CAST(500 AS Decimal(18, 0)), N'Charles Perrault', 5, CAST(N'2022-09-14' AS Date), N'Classic stories and fairy tales go hand in hand with a child’s growing up years. However, in this age of comic books, the classics are gradually finding less and less takers. Keeping this in mind we have selected 24 such all time favourite classics and translated them into graphic format. While remaining faithful to the original plot, these stories contain neat, pithy text and vivid, colourful graphics that make reading a pleasure.Children as well as adolescents will find this series to be a fascinating read, and it can help your child to make the ascension from cartoons to the classics.', 1, CAST(N'2022-09-14' AS Date), 5, CAST(N'2022-09-15' AS Date), 5)
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (13, N'Jungle Book', 1, CAST(460 AS Decimal(18, 0)), N'Disney', 5, CAST(N'2022-09-15' AS Date), N'The Jungle Book (1894) is a collection of stories by the English author Rudyard Kipling. Most of the characters are animals such as Shere Khan the tiger and Baloo the bear, though a principal character is the boy or "man-cub" Mowgli, who is raised in the jungle by wolves. The stories are set in a forest in India; one place mentioned repeatedly is "Seonee" (Seoni), in the central state of Madhya Pradesh.  A major theme in the book is abandonment followed by fostering, as in the life of Mowgli, echoing Kipling''s own childhood. The theme is echoed in the triumph of protagonists including Rikki-Tikki-Tavi and The White Seal over their enemies, as well as Mowgli''s. Another important theme is of law and freedom; the stories are not about animal behaviour, still less about the Darwinian struggle for survival, but about human archetypes in animal form. They teach respect for authority, obedience, and knowing one''s place in society with "the law of the jungle", but the stories also illustrate the freedom to move between different worlds, such as when Mowgli moves between the jungle and the village. Critics have also noted the essential wildness and lawless energies in the stories, reflecting the irresponsible side of human nature.  The Jungle Book has remained popular, partly through its many adaptations for film and other media. Critics such as Swati Singh have noted that even critics wary of Kipling for his supposed imperialism have admired the power of his storytelling.[1] The book has been influential in the scout movement, whose founder, Robert Baden-Powell, was a friend of Kipling''s.[2] Percy Grainger composed his Jungle Book Cycle around quotations from the book.', 1, CAST(N'2022-09-15' AS Date), 5, CAST(N'2022-09-15' AS Date), 5)
INSERT [dbo].[Book] ([BookId], [BookName], [CategoryId], [Price], [Publisher], [UserId], [publishedDate], [Description], [Active], [CreatedDate], [Createdby], [ModifiedDate], [Modifiedby]) VALUES (14, N'Avengers', 2, CAST(458 AS Decimal(18, 0)), N'Disney', 3, CAST(N'2022-09-21' AS Date), N'The Story of My Marriage is autobiographical with only names changed. The story is about a couple Krish and Ananya, who hail from two states of India, Punjab and Tamil Nadu, respectively, who are deeply in love and want to marry. It is narrated from a first-person point of view in a humorous tone, often taking digs at both Tamil and Punjabi cultures.  The story begins in the IIM Ahmedabad mess hall where Krish, a Punjabi boy from Delhi sights a beautiful girl Ananya, a Tamilian from Chennai quarreling with the mess staff about the food. Ananya was tagged as the "Best girl of the fresher batch". They become friends within a few days. Both graduate and get jobs with serious plans for their wedding. At first, Krish tries to convince his girlfriend Ananya''s parents by helping Ananya''s brother Manju IIT tuition and by helping her father Swaminathan create his first PowerPoint presentation. He later convinces her mom by helping her fulfill her biggest dream of singing at a concert by arranging for her to perform at', 1, CAST(N'2022-09-21' AS Date), 3, CAST(N'2022-09-21' AS Date), 3)
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (1, N'Comic')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (2, N'Fiction')
GO
INSERT [dbo].[Purchase] ([PurchaseId], [EmailId], [BookId], [PurchaseDate], [PaymentMode]) VALUES (1, N'meera@gmail.com', 6, CAST(N'2022-09-21' AS Date), N'Online')
INSERT [dbo].[Purchase] ([PurchaseId], [EmailId], [BookId], [PurchaseDate], [PaymentMode]) VALUES (2, N'simran@gmail.com', 4, CAST(N'2022-09-21' AS Date), N'Online')
INSERT [dbo].[Purchase] ([PurchaseId], [EmailId], [BookId], [PurchaseDate], [PaymentMode]) VALUES (3, N'simran@gmail.com', 10, CAST(N'2022-09-21' AS Date), N'Online')
INSERT [dbo].[Purchase] ([PurchaseId], [EmailId], [BookId], [PurchaseDate], [PaymentMode]) VALUES (4, N'mariya@gmail.com', 6, CAST(N'2022-09-21' AS Date), N'Online')
INSERT [dbo].[Purchase] ([PurchaseId], [EmailId], [BookId], [PurchaseDate], [PaymentMode]) VALUES (5, N'mariya@gmail.com', 3, CAST(N'2022-09-21' AS Date), N'Online')
INSERT [dbo].[Purchase] ([PurchaseId], [EmailId], [BookId], [PurchaseDate], [PaymentMode]) VALUES (6, N'mariya@gmail.com', 12, CAST(N'2022-09-21' AS Date), N'Online')
INSERT [dbo].[Purchase] ([PurchaseId], [EmailId], [BookId], [PurchaseDate], [PaymentMode]) VALUES (7, N'mariya@gmail.com', 10, CAST(N'2022-09-23' AS Date), N'Offline')
GO
INSERT [dbo].[RoleMaster] ([RoleId], [RoleName]) VALUES (1, N'Author')
INSERT [dbo].[RoleMaster] ([RoleId], [RoleName]) VALUES (2, N'Reader')
GO
INSERT [dbo].[User] ([UserId], [UserName], [EmailId], [Password], [RoleId], [Active], [FirstName], [LastName]) VALUES (1, N'simran', N'simran@gmail.com', N'MTIz', 2, 1, N'simran', N'simran')
INSERT [dbo].[User] ([UserId], [UserName], [EmailId], [Password], [RoleId], [Active], [FirstName], [LastName]) VALUES (2, N'meera', N'meera@gmail.com', N'MTIz', 2, 1, N'Meera', N'Dijo')
INSERT [dbo].[User] ([UserId], [UserName], [EmailId], [Password], [RoleId], [Active], [FirstName], [LastName]) VALUES (3, N'james', N'james@gmail.com', N'MTIz', 1, 1, N'James', N'Bond')
INSERT [dbo].[User] ([UserId], [UserName], [EmailId], [Password], [RoleId], [Active], [FirstName], [LastName]) VALUES (4, N'sam', N'sameera@gmail.com', N'MzIx', 2, 1, N'Sameera', N'Reddy')
INSERT [dbo].[User] ([UserId], [UserName], [EmailId], [Password], [RoleId], [Active], [FirstName], [LastName]) VALUES (5, N'jen', N'jennifer@gmail.com', N'MzIx', 1, 1, N'Jennifer', N'widget')
INSERT [dbo].[User] ([UserId], [UserName], [EmailId], [Password], [RoleId], [Active], [FirstName], [LastName]) VALUES (6, N'jack', N'jacquline@gmail.com', N'MzIx', 2, 1, N'Jacqualine', N'widget')
INSERT [dbo].[User] ([UserId], [UserName], [EmailId], [Password], [RoleId], [Active], [FirstName], [LastName]) VALUES (7, N'rahul', N'rahul@gmail.com', N'MTIz', 2, 1, N'Rahul', N'Iswar')
INSERT [dbo].[User] ([UserId], [UserName], [EmailId], [Password], [RoleId], [Active], [FirstName], [LastName]) VALUES (8, N'henna', N'henna@gmail.com', N'MTIzNDU2', 2, 1, N'henna', N'George')
INSERT [dbo].[User] ([UserId], [UserName], [EmailId], [Password], [RoleId], [Active], [FirstName], [LastName]) VALUES (9, N'dixon', N'dixon@gmail.com', N'NjU0MzIx', 1, 1, N'Dixon', N'Jose')
INSERT [dbo].[User] ([UserId], [UserName], [EmailId], [Password], [RoleId], [Active], [FirstName], [LastName]) VALUES (10, N'mariya', N'mariya@gmail.com', N'MTIzNDU2', 2, 1, N'Mariya', N'Jenson')
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Category]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_Book]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_RoleMaster] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleMaster] ([RoleId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_RoleMaster]
GO
