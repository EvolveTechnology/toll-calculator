export class Time {
  static fromDate(date: Date): Time {
    return new Time(date.getHours(), date.getMinutes());
  }

  constructor(public hour: number, public minute: number) {}

  public isAfter(time: Time): boolean {
    return (
      this.hour > time.hour ||
      (this.hour === time.hour && this.minute >= time.minute)
    );
  }

  public diffInMinutes(time: Time): number {
    return (this.hour - time.hour) * 60 + this.minute - time.minute;
  }
}
