import { Component, OnInit, ViewChild } from '@angular/core';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { PagingDTO } from 'src/app/shared/models/paging-dto';
import { ContactUs } from '../../models/contact-us';
import { ContactUsService } from '../../services/contact-us.service';
import { ContactUssearch } from '../../models/contact-us-search';

@Component({
  selector: 'app-contact-us-list',
  templateUrl: './contact-us-list.component.html',
  styleUrls: ['./contact-us-list.component.css']
})
export class ContactUsListComponent implements OnInit {
  @ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
  dataSource: PagingDTO = new PagingDTO();
  contactUsList: Array<ContactUs>;
  searchCriteriaDTO: ContactUssearch = new ContactUssearch()
  showFilterControls: boolean = false;
  total: number;
  recordsPerPage: number = 5;
  constructor(private contactUsService: ContactUsService) { }

  ngOnInit(): void {
    this.search();
  }
  toggleFilter() {
    this.searchCriteriaDTO = new ContactUssearch();
    this.showFilterControls = !this.showFilterControls;
  }
  getAll() {
    this.contactUsService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
      this.contactUsList = res.list;
      this.total = res.total;
      if (this.paginationComponent) {
        this.paginationComponent.totalRecordsCount = this.total;
        this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
      }
    });
  }
  search() {
    this.getAll();
  }
  onPageChange(event: any) {
    this.searchCriteriaDTO.page = event;
    this.getAll();
  }
}
