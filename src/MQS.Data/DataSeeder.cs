using Microsoft.Extensions.DependencyInjection;
using MQS.Application.Common.Interfaces;
using MQS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQS.Data
{
	public static class DataSeeder
	{
		public static async Task SeedAsync(IServiceProvider serviceProvider)
		{
			var authors = await SeedAuthorsAsync(serviceProvider);
			await SeedBooksAsync(serviceProvider, authors);
		}

		private static async Task SeedBooksAsync(IServiceProvider serviceProvider, List<Author> authors)
		{
			var random = new Random(DateTime.Now.Millisecond);
			var bookService = serviceProvider.GetRequiredService<IBookService>();

			// Do not insert new books if books already exist
			if (bookService.Count() > 0) return;

			var books = new[] {
				new Book { 
					Title = "JavaScript: The Definitive Guide, 6th Edition",
					Category = "Web Programming",
					Introduction = "JavaScript: The Definitive Guide has been the bible for JavaScript programmers-a programmer's guide and comprehensive reference to the core language and to the client-side JavaScript APIs defined by web browsers.",
					Price = 33.99m,
					Publisher = "O'Reilly Media",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/510JjoNTdOL._SX379_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9780596805524.png"
					}
				},
				new Book {
					Title = "Cracking Codes with Python",
					Category = "Programming Language",
					Introduction = "After a crash course in Python programming basics, you'll learn to make, test, and hack programs that encrypt text with classical ciphers like the transposition cipher and VigenÃ¨re cipher. You'll begin with simple programs for the reverse and Caesar ciphers and then work your way up to public key cryptography, the type of encryption used to secure today's online transactions, including digital signatures, email, and Bitcoin.",
					Price = 11.69m,
					Publisher = "No Starch Press",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/51ci5fe2mpL._SX376_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781593278229.png"
					}
				},
				new Book {
					Title = "Practical Machine Learning with Python",
					Category = "Machine Learning",
					Introduction = "Master the essential skills needed to recognize and solve complex problems with machine learning and deep learning. Using real-world examples that leverage the popular Python machine learning ecosystem, this book is your perfect companion for learning the art and science of machine learning to become a successful practitioner.",
					Price = 38.29m,
					Publisher = "Apress",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/41-NbAnk77L._SX348_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781484232064.png"
					}
				},
				new Book {
					Title = "Pro C# 7 With .NET and .NET Core",
					Category = "Programming Language",
					Introduction = "This essential classic title provides a comprehensive foundation in the C# programming language and the frameworks it lives in. Now in its 8th edition, you'll find all the very latest C# 7.1 and .NET 4.7 features here, along with four brand new chapters on Microsoft's lightweight, cross-platform framework, .NET Core, up to and including .NET Core 2.0. Coverage of ASP.NET Core, Entity Framework (EF) Core, and more, sits alongside the latest updates to .NET, including Windows Presentation Foundation (WPF), Windows Communication Foundation (WCF), and ASP.NET MVC.",
					Price = 13.33m,
					Publisher = "Apress",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/41Kze83E2ZL._SX348_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781484230176.png"
					}
				},
				new Book {
					Title = "Modern Java Recipes",
					Category = "Programming Language",
					Introduction = "The introduction of functional programming concepts in Java SE 8 was a drastic change for this venerable object-oriented language. Lambda expressions, method references, and streams fundamentally changed the idioms of the language, and many developers have been trying to catch up ever since. This cookbook will help. With more than 70 detailed recipes, author Ken Kousen shows you how to use the newest features of Java to solve a wide range of problems.",
					Price = 40.34m,
					Publisher = "O'Reilly Media",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/516JjBoVmVL._SX379_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781491973172.png"
					}
				},
				new Book {
					Title = "Learning TensorFlow",
					Category = "Machine Learning",
					Introduction = "Roughly inspired by the human brain, deep neural networks trained with large amounts of data can solve complex tasks with unprecedented accuracy. This practical book provides an end-to-end guide to TensorFlow, the leading open source software library that helps you build and train neural networks for computer vision, natural language processing (NLP), speech recognition, and general predictive analytics.",
					Price = 26.99m,
					Publisher = "O'Reilly Media",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/51M5mGcaexL._SX379_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781491978511.png"
					}
				},
				new Book {
					Title = "A Common-Sense Guide to Data Structures and Algorithms",
					Category = "Algorithms",
					Introduction = "Algorithms and data structures are much more than abstract concepts. Mastering them enables you to write code that runs faster and more efficiently, which is particularly important for today's web and mobile apps. This book takes a practical approach to data structures and algorithms, with techniques and real-world scenarios that you can use in your daily production code. Graphics and examples make these computer science concepts understandable and relevant. You can use these techniques with any language; examples in the book are in JavaScript, Python, and Ruby.",
					Price = 39.01m,
					Publisher = "The Pragmatic Programmers",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/41ifrhvheUL._SX404_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781680502442.png"
					}
				},
				new Book {
					Title = "Hands-On Dark Web Analysis",
					Category = "Web Programming",
					Introduction = "The overall world wide web is divided into three main areas - the Surface Web, the Deep Web, and the Dark Web. The Deep Web and Dark Web are the two areas which are not accessible through standard search engines or browsers. It becomes extremely important for security professionals to have control over these areas to analyze the security of your organization.",
					Price = 29.99m,
					Publisher = "Packt Publishing",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/418wr8Bt94L._SX404_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781789133363.png"
					}
				},
				new Book {
					Title = "Learning C++ by Building Games with Unreal Engine 4, 2nd Edition",
					Category = "Programming Language",
					Introduction = "Learning to program in C++ requires some serious motivation. Unreal Engine 4 (UE4) is a powerful C++ engine with a full range of features used to create top-notch, exciting games by AAA studios, making it the fun way to dive into learning C++17.",
					Price = 44.99m,
					Publisher = "Packt Publishing",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/51pijfFxtrL._SX404_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781788476249.png"
					}
				},
				new Book {
					Title = "DevOps: WTF?",
					Category = "DevOps",
					Introduction = "DevOps is creating a lot of anxiety amongst the IT professionals of the world. It's also causing a bit of a boom for technology marketing people, who in turn are making the whole concept confusing for businesses and professionals alike.",
					Price = 10,
					Publisher = "Leanpub",
					CoverImage = new Picture
					{
						Url = "",
						ThumbnailUrl = "https://itbook.store/img/books/1001592565453.png"
					}
				},
				new Book {
					Title = "Microservices in .NET, 2nd Edition",
					Category = "Microservices",
					Introduction = "Microservices in .NET, Second Edition is a comprehensive guide to building microservice applications using the .NET stack. After a crystal-clear introduction to the microservices architectural style, it teaches you practical microservices development skills using ASP.NET. This second edition of the bestselling original has been revised with up-to-date tools for the .NET ecosystem, and more new coverage of scoping microservices and deploying to Kubernetes.",
					Price = 59.99m,
					Publisher = "Manning",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/41r2c1hU07L._SX397_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781617297922.png"
					}
				},
				new Book {
					Title = "Spring Start Here",
					Category = "Programming Language",
					Introduction = "Spring Start Here introduces you to Java development with Spring by concentrating on the core concepts you'll use in every application you build. You'll learn how to refactor an existing application to Spring, how to use Spring tools to make SQL database requests and REST calls, and how to secure your projects with Spring Security. There's always more to learn, and this book will make your next steps much easier.",
					Price = 49.99m,
					Publisher = "Manning",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/419kGFitivL._SX397_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781617298691.png"
					}
				},
				new Book {
					Title = "The Programmer's Brain",
					Category = "Algorithms",
					Introduction = "Learn how to optimize your brain's natural cognitive processes to read code more easily, write code faster, and pick up new languages in much less time. This book will help you through the confusion you feel when faced with strange and complex code, and explain a codebase in ways that can make a new team member productive in days!",
					Price = 44.79m,
					Publisher = "Manning",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/41tgCgc378L._SX397_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781617298677.png"
					}
				},
				new Book {
					Title = "Graph-Powered Machine Learning",
					Category = "Machine Learning",
					Introduction = "Graph-Powered Machine Learning teaches to use graph-based algorithms and data organization strategies to develop superior machine learning applications. You'll dive into the role of graphs in machine learning and big data platforms, and take an in-depth look at data source modeling, algorithm design, recommendations, and fraud detection. Explore end-to-end projects that illustrate architectures and help you optimize with best design practices.",
					Price = 49.99m,
					Publisher = "Manning",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/419WmMZBSKL._SX397_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781617295645.png"
					}
				},
				new Book {
					Title = "HackSpace Magazine: Issue 47",
					Category = "Magazines",
					Introduction = "If your 3D printer is looking a little dusty and unloved, now's the time to put it to work: we've 50 of the best 3D prints to improve your home, office, workshop and more. From functional to frivolous, we've got ideas for you. It's time to unleash the awesome power of your printer!",
					Price = 15,
					Publisher = "Raspberry Pi Press",
					CoverImage = new Picture
					{
						Url = "",
						ThumbnailUrl = "https://itbook.store/img/books/1001635431011.png"
					}
				},
				new Book {
					Title = "97 Things Every Software Architect Should Know",
					Category = "Software Development",
					Introduction = "In this truly unique technical book, today's leading software architects present valuable principles on key development issues that go way beyond technology. More than four dozen architects - including Neal Ford, Michael Nygard, and Bill de hOra - offer advice for communicating with stakeholders, eliminating complexity, empowering developers, and many more practical lessons they've learned from years of experience.",
					Price = 29.74m,
					Publisher = "O'Reilly Media",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/51mz8lUV--L._SX331_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9780596522698.png"
					}
				},
				new Book {
					Title = "Architect Modern Web Applications with ASP.NET Core and Azure",
					Category = "Software Development",
					Introduction = "The audience for this guide is mainly developers, development leads, and architects who are interested in building modern web applications using Microsoft technologies and services in the cloud.",
					Price = 12,
					Publisher = "Microsoft Press",
					CoverImage = new Picture
					{
						Url = "",
						ThumbnailUrl = "https://itbook.store/img/books/1001635859865.png"
					}
				},
				new Book {
					Title = "Deep Learning with Python, 2nd Edition",
					Category = "Machine Learning",
					Introduction = "Deep Learning with Python has taught thousands of readers how to put the full capabilities of deep learning into action. This extensively revised second edition introduces deep learning using Python and Keras, and is loaded with insights for both novice and experienced ML practitioners. You'll learn practical techniques that are easy to apply in the real world, and important theory for perfecting neural networks.",
					Price = 53.99m,
					Publisher = "Manning",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/41TeljhIQtL._SX397_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781617296864.png"
					}
				},
				new Book {
					Title = "Pro Drupal 7 Development, 3rd Edition",
					Category = "CMS",
					Introduction = "Pro Drupal 7 Development updates the most popular development reference for the release of Drupal 7. With several new and completely-rewritten essential APIs and improvements in Drupal 7, this book will not only teach developers how to write modules ranging from simple to complex, but also how Drupal itself works.",
					Price = 24.96m,
					Publisher = "Apress",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/41VEB+lNwIL._SX404_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781430228387.png"
					}
				},
				new Book {
					Title = "Joomla! 3 Beginner's Guide",
					Category = "CMS",
					Introduction = "Joomla! is one of the most popular open source Content Management Systems, actively developed and supported by a world-wide user community. It's a free, fun, and feature-rich tool for anyone who wants to create dynamic, interactive websites. Even beginners can deploy Joomla to build professional websites. Even though it can be challenging to get beyond the basics and build the site that meets your needs perfectly, this book will guide you through it all.",
					Price = 6.49m,
					Publisher = "Packt Publishing",
					CoverImage = new Picture
					{
						Url = "https://images-na.ssl-images-amazon.com/images/I/41vA2fe2q8L._SX404_BO1,204,203,200_.jpg",
						ThumbnailUrl = "https://itbook.store/img/books/9781782164340.png"
					}
				},
			};

			foreach (var item in books)
			{
				var authorIdx = random.Next(authors.Count);
				item.AuthorId = authors[authorIdx].Id;
			}

			await bookService.InsertRangeAsync(books);
		}

		private static async Task<List<Author>> SeedAuthorsAsync(IServiceProvider serviceProvider)
		{
			var authorService = serviceProvider.GetRequiredService<IAuthorService>();
			var authors = await authorService.GetAllAsync();

			if (authors.Any()) return authors;

			authors = new List<Author> {
				new Author {FirstName = "David", LastName="Flanagan"},
				new Author {FirstName = "Al", LastName="Sweigart"},
				new Author {FirstName = "Tushar", LastName="Sharma"},
				new Author {FirstName = "Andrew", LastName="Troelsen"},
				new Author {FirstName = "Ken", LastName="Kousen"},
				new Author {FirstName = "Itay", LastName="Lieder"},
				new Author {FirstName = "Jay", LastName="Wengrow"},
				new Author {FirstName = "Sion", LastName="Retzkin"},
				new Author {FirstName = "Sharan", LastName="Volin"},
				new Author {FirstName = "Don", LastName="Jones"},
			};
			await authorService.InsertRangeAsync(authors);

			return authors;
		}
	}
}
