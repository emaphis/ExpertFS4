// Mutually Recursive Type

// Book is dependent on Pages and Pages are dependent on Book

open System.Collections.Generic

type Book() =
    let pages = List<Page>()
    member x.Pages with get() = pages.AsReadOnly()
    member x.AddPage(pageNumber: int, page: Page) =
        if page.Owner = Some(x) then failwith "Page is already part of a book"
        pages.Insert(pageNumber - 1, page)
and Page(content: string) =
    let mutable owner: Book option = None
    member x.Content = content
    member x.Owner with get() = owner
    override x.ToString() = content
