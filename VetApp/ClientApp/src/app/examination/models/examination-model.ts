export class Doctor {
  name: string;
}
export class Examination {
  id: number;
  notes: string;

}

export class AppointmentWithExaminations {
  examinations: Examination[];
}
