using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Video video1 = new Video("How to Make Pancakes", "CookingGuy", 420);
        video1.AddComment(new Comment("Alex", "These turned out great!"));
        video1.AddComment(new Comment("Sarah", "Very easy to follow."));
        video1.AddComment(new Comment("Mike", "I added chocolate chips!"));

        Video video2 = new Video("C# Classes Explained", "CodeTeacher", 650);
        video2.AddComment(new Comment("Emma", "This helped a lot."));
        video2.AddComment(new Comment("James", "Finally understand classes."));
        video2.AddComment(new Comment("Liam", "Great explanation."));
        video2.AddComment(new Comment("Olivia", "Thanks for the tutorial!"));

        Video video3 = new Video("Best Jungle Tips", "GamingPro", 530);
        video3.AddComment(new Comment("Kevin", "This improved my games."));
        video3.AddComment(new Comment("Chris", "The pathing tip was useful."));
        video3.AddComment(new Comment("Jordan", "Can you do another guide?"));

        List<Video> videos = new List<Video>
        {
            video1,
            video2,
            video3
        };

        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"{comment.GetName()}: {comment.GetText()}");
            }

            Console.WriteLine();
        }
    }
}