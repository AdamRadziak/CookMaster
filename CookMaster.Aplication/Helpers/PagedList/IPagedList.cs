﻿namespace CookMaster.Aplication.Helpers.PagedList
{
    public interface IPagedList<out T, TOut> : IPagedList<TOut>
    {
        /*
        TOut this[int index] { get; }
        int Count { get; }
        */
        IPagedList<TOut> GetMetaData();
    }

    public interface IPagedList<TOut>
    {
        int PageCount { get; }

        int TotalItemCount { get; }

        int PageNumber { get; }

        int PageSize { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }

        bool IsFirstPage { get; }

        bool IsLastPage { get; }

        int FirstItemOnPage { get; }

        int LastItemOnPage { get; }

        public IEnumerable<TOut> PageData { get; }
    }
}
