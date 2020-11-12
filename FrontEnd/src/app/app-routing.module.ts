import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SpotSearchComponent } from '../components/spot-search/spot-search.component';
import { ToolBarComponent } from '../components/tool-bar/tool-bar.component';
import { AccommodationsSearchComponent} from '../components/accommodations-search/accommodations-search.component';
import { UsersManagementComponent } from 'src/components/users-management/users-management.component';

const routes: Routes = [
  { path: 'spot-search', component: SpotSearchComponent },
  { path: 'accommodation-search/:spotId', component: AccommodationsSearchComponent },
  { path: 'users-management', component: UsersManagementComponent }
  //, canActivate: [BeerNotExistGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}