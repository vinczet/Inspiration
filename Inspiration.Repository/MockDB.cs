using Inspiration.Repository.DataAccessObjects;

namespace Inspiration.Repository
{
    public static class MockDB
    {
        private static Guid npu1Id = Guid.Parse("dc3610be-2740-4dd4-9037-794d534a2d4b");
        private static Guid npu2Id = Guid.Parse("41881b3d-dc3d-4979-8eb1-97cf718cec9d");
        private static Guid npu3Id = Guid.Parse("f6657e08-9e78-44a1-8cd5-81605aa4a9e9");

        private static Guid user1Id = Guid.Parse("5dc2e723-8682-4d18-b343-9773cecb518a");
        private static Guid user2Id = Guid.Parse("a0d32145-1545-434a-a254-817775f77139");

        public static List<NPUDao> NPUs { get; set; } = FillInitialNPUs();
        public static List<UserDao> Users { get; set; } = FillInitialUsers();
        public static List<PartDao> Parts { get; set; } = FillInitialParts();
        public static List<CreativityRatingDao> CreativityRatings { get; set; } = FillInitialCreativityRatings();
        public static List<UniquenessRatingDao> UniquenessRatings { get; set; } = FillInitialUniquenessRatings();

        private static List<NPUDao> FillInitialNPUs()
        {
            var list = new List<NPUDao>();

            list.Add(new NPUDao(npu1Id, user1Id, "Nice pink frogs on the bonsai", "fac60102-6b52-4bcf-a9ef-43f4c638fdb1.png", true, new DateTime(2023, 1, 2, 3, 4, 5, DateTimeKind.Utc), new DateTime(2023, 2, 3, 4, 5, 6, DateTimeKind.Utc)));
            list.Add(new NPUDao(npu2Id, user1Id, "Nice bonsai on pink frogs", "c98f6ce8-9a82-4bf2-93f4-5e1756cc3232.jpeg", true, new DateTime(2023, 4, 5, 3, 4, 5, DateTimeKind.Utc), new DateTime(2023, 5, 13, 4, 5, 6, DateTimeKind.Utc)));
            list.Add(new NPUDao(npu3Id, user2Id, "This content is disabled", "2f589947-2399-4125-a559-85b0e443e950.jpeg", false, new DateTime(2023, 6, 5, 3, 4, 5, DateTimeKind.Utc), new DateTime(2023, 5, 13, 4, 5, 6, DateTimeKind.Utc)));

            return list;
        }

        private static List<UserDao> FillInitialUsers()
        {
            var list = new List<UserDao>();

            list.Add(new UserDao(user1Id, "Han Solo", "han.solo@starwars.com", "nottoostrong", false));
            list.Add(new UserDao(user2Id, "Vincze Tamás", "vinczetamas@hotmail.com", "stronger", true));

            return list;
        }

        private static List<PartDao> FillInitialParts()
        {
            var list = new List<PartDao>();

            list.Add(new PartDao(Guid.Parse("f9e8040f-d781-4e2f-9a45-0cb47f123845"), npu1Id, "frog"));
            list.Add(new PartDao(Guid.Parse("a0d32145-1545-434a-a254-817775f77139"), npu1Id, "bonsai"));
            list.Add(new PartDao(Guid.Parse("a88f5977-f704-492c-b71f-f18313b58bb5"), npu2Id, "frog"));
            list.Add(new PartDao(Guid.Parse("1667fcb6-92c2-4a03-ac0b-d767c99e729f"), npu2Id, "bonsai"));

            return list;
        }

        private static List<CreativityRatingDao> FillInitialCreativityRatings()
        {
            var list = new List<CreativityRatingDao>();

            list.Add(new CreativityRatingDao(Guid.Parse("9cdb845a-e641-42c3-bb5d-69e8a9062327"), npu1Id, user1Id, 3));
            list.Add(new CreativityRatingDao(Guid.Parse("300fc99e-140a-45e7-aefb-c2f691d872d8"), npu1Id, user2Id, 4));
            list.Add(new CreativityRatingDao(Guid.Parse("5ae292fc-72ab-4fce-850f-43f8a7e262da"), npu2Id, user2Id, 5));
            list.Add(new CreativityRatingDao(Guid.Parse("cd057e35-9871-4343-adca-ee8c59eeeaff"), npu3Id, user1Id, 4));

            return list;
        }

        private static List<UniquenessRatingDao> FillInitialUniquenessRatings()
        {
            var list = new List<UniquenessRatingDao>();

            list.Add(new UniquenessRatingDao(Guid.Parse("83757bea-f9f0-4d70-98ad-5314421e72bb"), npu1Id, user1Id, 3));
            list.Add(new UniquenessRatingDao(Guid.Parse("bbcf6e6f-5d5a-486b-8047-07255f45e1bd"), npu2Id, user1Id, 4));
            list.Add(new UniquenessRatingDao(Guid.Parse("d87704d6-0b9d-4df3-8bda-8070dd7b53e3"), npu2Id, user2Id, 5));
            list.Add(new UniquenessRatingDao(Guid.Parse("6925b043-bb61-490d-8cce-ea31a26ae318"), npu3Id, user2Id, 4));

            return list;
        }

    }
}
