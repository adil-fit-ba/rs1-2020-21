
export abstract class PagingInfo
{
  public currentPage =1;
  public itemsPerPage: number=10;
  public abstract getTotalItems():number;

  public pageNumbersArray():number[]{
    let result=[];

      for (let i = 0; i < this.totalPages(); i++)
        result.push(i+1);
    return result;
  }

  public totalPages() {
    return this.getTotalItems() / this.itemsPerPage;
  }

  goToPage(p: number) {
    this.currentPage = p;
    this.afterClicked();
  }

  goPrev() {
    if (this.currentPage>1)
      this.currentPage--;

    this.afterClicked();
  }

  goNext() {
    if (this.currentPage<this.totalPages())
      this.currentPage++;

    this.afterClicked();
  }

  public abstract afterClicked() ;
}
