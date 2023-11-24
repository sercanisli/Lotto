﻿namespace Entities.DataTransferObjects
{
    public record SansTopuDtoForUpdate
    {
        public int Id { get; init; }
        public List<int> Numbers { get; init; }
        public int PlusNumber { get; init; }
        public DateTime Date { get; init; }
    }
}
