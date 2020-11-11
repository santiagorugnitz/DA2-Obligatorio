import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SpotSearchComponent } from '../components/spot-search/spot-search.component';
import { ToolBarComponent } from '../components/tool-bar/tool-bar.component';
import { AccommodationsSearchComponent} from '../components/accommodations-search/accommodations-search.component';
import { ReservationComponent} from '../components/reservation/reservation.component';


const routes: Routes = [
  { path: 'spot-search', component: SpotSearchComponent },
  { path: 'accommodation-search/:spotId', component: AccommodationsSearchComponent },
  { path: 'reservations/:id', component: ReservationComponent }

  //, canActivate: [BeerNotExistGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}