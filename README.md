# ⏱ Quartz.NET + MassTransit + RabbitMQ POC

## 📘 Overview

This proof-of-concept (POC) demonstrates how to implement **recurring message scheduling** using **RabbitMQ** as the message broker and In-memory store. It highlights how to:

- Define and publish recurring schedules.
    
- Consume scheduled messages.
    
- Use Quartz.NET clustering and persistence.
    
- Integrate MassTransit with RabbitMQ.
    

---

## 🧠 Theoretical Background

### Quartz.NET

Quartz.NET is a full-featured, open source job scheduling system that can be used to execute tasks at specific intervals or times.

**Key Concepts**:

- **Scheduler**: Core service that manages jobs.
    
- **Job**: The task logic that runs.
    
- **Trigger**: The schedule that fires the job (e.g., CRON).
    
- **Persistent Store**: Saves job state across restarts.
    
- **Clustering**: Allows multiple instances to coordinate job execution for HA.
    

### MassTransit

MassTransit is a .NET service bus for building distributed applications.

**Key Concepts**:

- **Consumers**: Receive and handle messages.
    
- **Messages**: Contracts (DTOs) passed between services.
    
- **Scheduler**: MassTransit can use Quartz to schedule messages (recurring or delayed).
    
- **Transport**: RabbitMQ is used to publish and consume messages.
    

---

## ⚙️ Architecture

```
Quartz Scheduler (Clustered)
        |
        +----> SQL Server (Persistent Store)
        |
MassTransit + Quartz Integration
        |
        +----> RabbitMQ
                  |
                  +----> Consumer 1 (SchedulOrderConsumer)
                  +----> Consumer 2 (SubmitOrderConsumer)
```

---

## 🏗 Project Structure

|File|Purpose|
|---|---|
|`Program.cs`|App bootstrap, scheduler setup, message publishing|
|`SubmitOrderSchedule.cs`|Defines CRON-based recurring schedule|
|`Messages/SubmitOrder.cs`|SubmitOrder message definition|
|`Messages/SchedulOrder.cs`|SchedulOrder message definition|
|`SubmitOrderConsumer.cs`|Logs SubmitOrder messages|
|`SchedulOrderConsumer.cs`|Logs SchedulOrder messages|
