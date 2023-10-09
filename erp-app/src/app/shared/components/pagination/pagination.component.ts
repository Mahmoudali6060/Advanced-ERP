import { HttpClient } from '@angular/common/http';
import {
  Component,
  Input,
  EventEmitter,
  Output,
  OnInit,
  SimpleChange,
} from '@angular/core';
import { PagerService } from '../../services/pager.service';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
})
export class PaginationComponent implements OnInit {

  //#region Variables
  // Total records count
  @Input() totalRecordsCount: number = 0;
  // Records per page
  @Input() recordsPerPage: number = 30;
  //When User changes the page number
  @Output() pageChange = new EventEmitter<number>();

  // Total pages
  totalPages: number = 0;
  // Current page number
  currentPageNumber: number = 1;
  // Pager
  pager: any = {};
  //#endregion

  constructor(private http: HttpClient, private pagerService: PagerService) { }

  // On init
  ngOnInit() {
    this.setPagination(1);
  }


  // Fetch new page data
  next() {
    this.pageChange.emit(this.currentPageNumber + 1);
    this.setPagination(this.currentPageNumber + 1);
  }

  // Fetch previous page data
  prev() {
    this.pageChange.emit(this.currentPageNumber - 1);
    this.setPagination(this.currentPageNumber - 1);
  }

  // Fetch data from API
  getData(pageNo: any) {
    this.pageChange.emit(pageNo);
    this.setPagination(pageNo);
  }

  // Set pagination data and pager data
  setPagination(pageNo: any) {
    pageNo = Number(pageNo);
    this.currentPageNumber = pageNo;
    this.totalPages = Math.ceil(this.totalRecordsCount / this.recordsPerPage);
    this.pager = this.pagerService.getPager(
      this.totalRecordsCount,
      pageNo,
      this.recordsPerPage
    );
  }

}
