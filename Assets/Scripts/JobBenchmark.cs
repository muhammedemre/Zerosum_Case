using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

/*
 *
 * JobBenchmark scene runs very slow because of the repeated dummy math operation below.
 * Implement the for loop below, using parallelized Unity jobs and Burst compiler to gain performance.
 * Is there a better math library for Burst jobs than UnityEngine.Mathf? 
 * If the 'count' is too large for your machine to handle, you can decrease it.
 * 
 */


// My Job system solution is ten time slower than the normal system xD . I need more expertise on this field.
public class JobBenchmark : MonoBehaviour
{
	[SerializeField]
	private bool useJob = false;

	private int count = 1000;

	private float[] values;

	void Start()
	{
		values = new float[count];
	}

	void Update()
	{
		if (useJob)
		{
			// Job here
			
			for (int i = 0; i < values.Length; i++)
			{
				JobCalculations(i);
			}
		}
		else
		{
			for (int i = 0; i < values.Length; i++)
			{
				//print("values[i] : " + values[i]);
				values[i] = Mathf.Sqrt(Mathf.Pow(values[i] + 1.75f, 2.5f + i)) * 5 + 2f;
			}
		}
	}

	void JobCalculations(int i)
	{
		// Instantiate
		FirstAddition firstAddition = new FirstAddition();
		SecondAddition secondAddition = new SecondAddition();
		GetThePower getThePower = new GetThePower();
		Multiply multiply = new Multiply();
		ThirdAddition thirdAddition = new ThirdAddition();
		GetSquareRoot getSquareRoot = new GetSquareRoot();

		
		firstAddition.firstNumber = values[i];
		firstAddition.secondNumber = 1.75f;
		firstAddition.firstAdditionResult = new NativeArray<float>(1, Allocator.TempJob);

		secondAddition.firstNumber = 2.5f;
		secondAddition.secondNumber = i;
		secondAddition.secondAdditionResult = new NativeArray<float>(1, Allocator.TempJob);
		
		getThePower.getThePowerResult = new NativeArray<float>(1, Allocator.TempJob);

		multiply.getTheMultiplicationResult = new NativeArray<float>(1, Allocator.TempJob);

		thirdAddition.thirdAdditionResult = new NativeArray<float>(1, Allocator.TempJob);

		getSquareRoot.getSquareRootResult = new NativeArray<float>(1, Allocator.TempJob);



		JobHandle handleFirstAddition = firstAddition.Schedule();
		JobHandle handleSecondAddition = secondAddition.Schedule();

		handleFirstAddition.Complete();
		handleSecondAddition.Complete();
		getThePower.firstNumber = firstAddition.firstAdditionResult[0];
		getThePower.secondNumber = secondAddition.secondAdditionResult[0];
		var getThePowerDependencies = JobHandle.CombineDependencies(handleFirstAddition, handleSecondAddition);
		JobHandle handleGetThePower = getThePower.Schedule(getThePowerDependencies);
		
		handleGetThePower.Complete();
		

		multiply.firstNumber = getThePower.getThePowerResult[0];
		multiply.secondNumber = 5f;
		

		JobHandle multiplicationHandle = multiply.Schedule(handleGetThePower);
		multiplicationHandle.Complete();

		thirdAddition.firstNumber = multiply.getTheMultiplicationResult[0];
		thirdAddition.secondNumber = 2f;
		JobHandle thirdAdditionHandle = thirdAddition.Schedule(multiplicationHandle);
		thirdAdditionHandle.Complete();

		getSquareRoot.firstNumber = thirdAddition.thirdAdditionResult[0];
		JobHandle getSquareRootHandle = getSquareRoot.Schedule(thirdAdditionHandle);
		getSquareRootHandle.Complete();


		firstAddition.firstAdditionResult.Dispose();
		secondAddition.secondAdditionResult.Dispose();
		getThePower.getThePowerResult.Dispose();
		multiply.getTheMultiplicationResult.Dispose();
		thirdAddition.thirdAdditionResult.Dispose();
		getSquareRoot.getSquareRootResult.Dispose();
	}
	private struct FirstAddition : IJob
	{
		public float firstNumber, secondNumber;
		public NativeArray<float> firstAdditionResult;
		public void Execute()
		{
			firstAdditionResult[0] = firstNumber + secondNumber;
		}
	}
	private struct SecondAddition : IJob
	{
		public float firstNumber, secondNumber;
		public NativeArray<float> secondAdditionResult;
		public void Execute()
		{
			secondAdditionResult[0] = firstNumber + secondNumber;
		}
	}

	private struct GetThePower : IJob
	{
		public float firstNumber, secondNumber;
		public NativeArray<float> getThePowerResult;
		public void Execute()
		{
			float tempFloat = Mathf.Pow(firstNumber, secondNumber);
			getThePowerResult[0] = tempFloat;
		}
	}

	private struct Multiply : IJob
	{
		public float firstNumber, secondNumber;
		public NativeArray<float> getTheMultiplicationResult;
		public void Execute()
		{
			getTheMultiplicationResult[0] = firstNumber * secondNumber;
		}
	}
	private struct ThirdAddition : IJob
	{
		public float firstNumber, secondNumber;
		public NativeArray<float> thirdAdditionResult;
		public void Execute()
		{
			thirdAdditionResult[0] = firstNumber + secondNumber;
		}
	}

	private struct GetSquareRoot : IJob
	{
		public float firstNumber;
		public NativeArray<float> getSquareRootResult;
		public void Execute()
		{
			getSquareRootResult[0] = Mathf.Sqrt(firstNumber);
		}
	}

}
