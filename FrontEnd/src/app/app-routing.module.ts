import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SpotSearchComponent } from '../components/spot-search/spot-search.component';
import { ToolBarComponent } from '../components/tool-bar/tool-bar.component';
import { AccommodationsSearchComponent} from '../components/accommodations-search/accommodations-search.component';
import { UsersManagementComponent } from 'src/components/users-management/users-management.component';
import { SpotReportComponent } from 'src/components/spot-report/spot-report.component';
import { AccommodationManagementComponent } from 'src/components/accommodation-management/accommodation-management.component';
import { SpotNotExistsGuard } from 'src/components/guards/spot-not-exists.guard';
import { UserNotLogguedGuard } from 'src/components/guards/user-not-loggued.guard';


const routes: Routes = [
  { path: 'spot-search', component: SpotSearchComponent },
  { path: 'accommodation-search/:spotId', component: AccommodationsSearchComponent, canActivate: [SpotNotExistsGuard] },
  { path: 'users-management', component: UsersManagementComponent, canActivate: [UserNotLogguedGuard] },
  { path: 'spot-report/:spotId', component: SpotReportComponent, canActivate: [SpotNotExistsGuard, UserNotLogguedGuard]  },
  { path: 'accommodation-management', component: AccommodationManagementComponent, canActivate: [UserNotLogguedGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}