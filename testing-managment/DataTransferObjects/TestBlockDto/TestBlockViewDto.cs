﻿namespace testing_managment.DataTransferObjects.TestBlockDto
{
    public class TestBlockViewDto
    {
        public Guid Id { get; set; }

        public Guid TestProgram { get; set; }

        public Guid TestGroup { get; set; }

        public Guid TestEngineer { get; set; }

        public DateOnly Deadline { get; set; }

        public string Recommendation { get; set; }
    }
}