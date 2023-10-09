import { Component, OnInit } from '@angular/core';
import { AboutUs } from '../../models/about-us';
import { AboutUsService } from '../../services/about-us.service';

@Component({
  selector: 'app-about-us',
  templateUrl: './about-us.component.html',
  styleUrls: ['./about-us.component.css']
})
export class AboutUsComponent implements OnInit {
  aboutUsList: Array<AboutUs>;
  aboutUsSearch : AboutUs
  constructor(private aboutUsService: AboutUsService) { }

  ngOnInit(): void {
    this.aboutUsSearch = new AboutUs();
    this.search();
  }
  getAll() {
    this.aboutUsService.getAll(this.aboutUsSearch).subscribe((res: any) => {
      this.aboutUsList = res.list;
    });
  }
  search() {
    this.getAll();
  }
}
