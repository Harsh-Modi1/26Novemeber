export class FilterView {
    constructor() {
        this.SortBy = '';
        this.Search = '';
        this.MaxPrice = null;
        this.MinPrice = null;
    }
    SortBy: string;
    Search: string;
    MaxPrice: number;
    MinPrice: number;
}
