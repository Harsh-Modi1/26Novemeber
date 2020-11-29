import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
name : 'productfilter'
})

export class ProductfilterPipe implements PipeTransform
{
transform(productList: any= [], searchTerm: string): any[] {
if (!productList || !searchTerm){
return productList;
}
return productList.filter(pro => pro.ProductName.toLowerCase().indexOf(searchTerm.toLowerCase()) !== -1
                            ||
                            pro.ProductDescription.toLowerCase().indexOf(searchTerm.toLowerCase()) !== -1);

}
}
