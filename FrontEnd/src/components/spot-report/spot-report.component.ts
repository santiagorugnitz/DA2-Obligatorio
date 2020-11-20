import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TouristSpot } from 'src/models/tourist-spot';
import { ReportItem } from 'src/models/report-item';
import { ReportService } from 'src/services/report.service';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { TouristSpotService } from 'src/services/tourist-spot.service';

@Component({
  selector: 'app-spot-report',
  templateUrl: './spot-report.component.html',
  styleUrls: ['./spot-report.component.css']
})
export class SpotReportComponent implements OnInit {
  
  startingDate = new Date();
  finishingDate = new Date();
  spot: TouristSpot;
  hasSearched: boolean;
  reportList: ReportItem[]

  constructor(private currentRoute: ActivatedRoute, private spotService: TouristSpotService, private reportService: ReportService) {
    this.hasSearched = false
   }

  ngOnInit(): void {
    let id = +this.currentRoute.snapshot.params['spotId'];
    this.spot = this.spotService.getSpotById(id)
  }

  changeStartingDate(event: MatDatepickerInputEvent<Date>){
    this.startingDate = event.value
  }

  changeFinishingDate(event: MatDatepickerInputEvent<Date>){
    this.finishingDate = event.value
  }

  search(){
    if(!this.hasSearched){
      if(this.startingDate <= this.finishingDate){
        this.reportList = this.reportService.getAccommodationsForReport(this.spot.id, this.startingDate, this.finishingDate)
        this.hasSearched = !this.hasSearched
      }else{
        alert('The dates are incorrect');
      }
    }else{
      this.hasSearched = !this.hasSearched
      this.reportList = []
    }  
  }

}
